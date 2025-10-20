using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public bool IsEnemy { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            IsEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            IsEnemy = false;
        }
    }
}
