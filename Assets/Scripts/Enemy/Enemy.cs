using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Chaser _enemyChaser;
    [SerializeField] private Patroller _enemyPatroller;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private Attacker _enemyAttacker;
    [SerializeField] private Health _enemyHealth;

    private void FixedUpdate()
    {
        if (_playerDetector.IsPlayerNearby)
        {
            _enemyChaser.MoveToTarget(_playerDetector.TargetPosition.x);

            if (_playerDetector.IsPlayer) 
            {
                _enemyAnimator.SetupAttack();
                _enemyAttacker.DealDamage(_playerDetector.DetectedPlayer.gameObject);      
            }

            _rotator.Rotate(_enemyChaser.DirectionX);
        }
        else 
        {
            _enemyPatroller.MoveToPoint();
            _rotator.Rotate(_enemyPatroller.DirectionX);
        }
    }

    private void Update()
    {
        _enemyAnimator.SetupSpeed();
    }
}