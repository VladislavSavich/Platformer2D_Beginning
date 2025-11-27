using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public Enemy DetectedEnemy { get; private set; }
    public bool IsEnemy { get; private set; }
    public bool IsEnemyInRadius { get; private set; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            IsEnemy = true;
            SetTarget(enemy);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            IsEnemy = false;
            ClearTarget();
        }
    }

    private void SetTarget(Enemy enemy)
    {
        DetectedEnemy = enemy;
    }

    private void ClearTarget() => DetectedEnemy = null;
}
