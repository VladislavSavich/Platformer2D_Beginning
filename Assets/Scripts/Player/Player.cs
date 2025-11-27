using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Collector _collector;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Attacker _playerAttacker;
    [SerializeField] private HealthStealer _healthStealer;

    public Vector2 Position => transform.position;

    private void OnEnable()
    {
        _collector.HealTaken += _playerHealth.TakeHeal;
    }

    private void OnDisable()
    {
        _collector.HealTaken -= _playerHealth.TakeHeal;
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0) 
        {
            _playerMover.Move(_inputReader.Direction);
            _rotator.Rotate(_inputReader.Direction);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGrounded)
            _playerMover.Jump();

        if (_inputReader.GetIsAttack() && _enemyDetector.IsEnemy) 
        {
            _playerAnimator.SetupAttack();
            _playerAttacker.DealDamage(_enemyDetector.DetectedEnemy.gameObject);
        }

        if (_inputReader.GetIsSkill()) 
        {
            _healthStealer.StealHealth();
        }
    }

    private void Update()
    {
        _playerAnimator.SetupSpeed();
    }
}
