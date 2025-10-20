using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private EnemyCombat _enemyCombat;

    private void FixedUpdate()
    {
        if (_playerDetector.IsPlayerNearby)
        {
            _enemyMover.MoveToTarget(_playerDetector.TargetPosition.x);

            if (_playerDetector.IsPlayer) 
            {
                _enemyAnimator.SetupAttack();
                _enemyCombat.DealDamage(_playerDetector.DetectedPlayer);      
            }
        }
        else 
        {
            _enemyMover.MoveToPoint();
        }

        _rotator.Rotate(_enemyMover.DirectionX);
    }

    private void Update()
    {
        _enemyAnimator.SetupSpeed();
    }
}