using UnityEngine;

public class NearestEnemySearcher : MonoBehaviour
{
    [SerializeField] private float _skillRadius = 5;

    private int _maximumNearbyEnemies = 20;
    private int _enemyCount;
    private Collider2D[] _allEnemiesInRadius;

    private void Start()
    {
        _allEnemiesInRadius = new Collider2D[_maximumNearbyEnemies];
    }

    public Enemy FindNearestEnemy()
    {
        _enemyCount = Physics2D.OverlapCircleNonAlloc(transform.position, _skillRadius, _allEnemiesInRadius);
        Enemy nearestEnemy = null;
        float nearestSqrDistance = float.MaxValue;

        for (int i = 0; i < _enemyCount; i++)
        {
            Collider2D collider = _allEnemiesInRadius[i];

            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                float sqrDistance = (enemy.transform.position - transform.position).sqrMagnitude;

                if (sqrDistance < nearestSqrDistance)
                {
                    nearestSqrDistance = sqrDistance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
