using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _currentSpeed;

    public readonly int Speed = Animator.StringToHash(nameof(Speed));
    public readonly int Attack = Animator.StringToHash(nameof(Attack));

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _currentSpeed = _rigidbody.velocity.magnitude;
    }

    public void SetupSpeed()
    {
        _animator.SetFloat(Speed, _currentSpeed);
    }

    public void SetupAttack() 
    {
        _animator.SetTrigger(Attack);
    }
}