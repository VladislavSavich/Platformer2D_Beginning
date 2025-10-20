using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private Vector2[] _patrolPoints;
    [SerializeField] private float _speed;

    private int _pointIndex = 0;
    private Vector2 _currentPoint;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    public float DirectionX { get; private set; }

    private void Start()
    {
        _currentPoint = _patrolPoints[_pointIndex];
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    public void MoveToPoint()
    {
        DirectionX = Mathf.Sign(_currentPoint.x - transform.localPosition.x);
        _rigidbody.velocity = new Vector2(DirectionX * _speed, _rigidbody.velocity.y);

        if (Mathf.Abs(transform.localPosition.x - _currentPoint.x) < 0.1f)
        {
            _pointIndex = (_pointIndex == 1) ? 0 : 1;
            _currentPoint = _patrolPoints[_pointIndex];
        }
    }
}
