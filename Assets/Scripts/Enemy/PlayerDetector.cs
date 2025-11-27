using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _searchRadius = 6f;
    
    public Player DetectedPlayer { get; private set; }
    public bool IsPlayer { get; private set; }
    public bool IsPlayerNearby { get; private set; }
    public Vector2 TargetPosition => DetectedPlayer.Position;

    private void Update()
    {
        FindPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            IsPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            IsPlayer = false;
        }
    }

    private void FindPlayer() 
    {
        Collider2D[] playersInRadius = Physics2D.OverlapCircleAll(transform.position, _searchRadius);
        bool playerFound = false;

        foreach (Collider2D playerCollider in playersInRadius)
        {
            if (playerCollider.TryGetComponent<Player>(out Player player))
            {
                playerFound = true;
                IsPlayerNearby = true;
                SetTarget(player);
                break;
            }
        }

        if (!playerFound && IsPlayerNearby)
        {
            IsPlayerNearby = false;
            UpdateTargetState();
        }
    }

    private void UpdateTargetState()
    {
        if (!IsPlayer && !IsPlayerNearby)
        {
            ClearTarget();
        }
    }

    private void SetTarget(Player player)
    {
        DetectedPlayer = player;
    }

    private void ClearTarget() => DetectedPlayer = null;
}
