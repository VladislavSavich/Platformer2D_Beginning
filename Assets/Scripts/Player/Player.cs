using System;
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
    [SerializeField] private PlayerCombat _playerCombat;

    public Vector2 Position => transform.position;

    private void OnEnable()
    {
        _collector.HealTaken += _playerCombat.TakeHeal;
    }

    private void OnDisable()
    {
        _collector.HealTaken -= _playerCombat.TakeHeal;
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
            _playerAnimator.SetupAttack();
    }

    private void Update()
    {
        _playerAnimator.SetupSpeed();
    }
}
