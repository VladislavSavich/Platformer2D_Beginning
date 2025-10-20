using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    public float DirectionX { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveToTarget(float targetPositionX)
    {
        DirectionX = Mathf.Sign(targetPositionX - transform.localPosition.x);
        _rigidbody.velocity = new Vector2(DirectionX * _speed, _rigidbody.velocity.y);
    }
}
