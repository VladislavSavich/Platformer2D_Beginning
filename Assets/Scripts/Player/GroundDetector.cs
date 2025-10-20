using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _groundEnteres = 0;
    public bool IsGrounded { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _groundEnteres++;

        if (collision.gameObject.TryGetComponent<Land>(out _) && _groundEnteres > 0)
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _groundEnteres--;

        if (collision.gameObject.TryGetComponent<Land>(out _) && _groundEnteres <= 0)
        {
            IsGrounded = false;
        }
    }
}