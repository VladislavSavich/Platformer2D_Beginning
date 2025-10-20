using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    public Player DetectedPlayer { get; private set; }
    public bool IsPlayer { get; private set; }
    public bool IsPlayerNearby { get; private set; }
    public Vector2 TargetPosition => DetectedPlayer.Position;

    private void Awake()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            IsPlayerNearby = true;
            SetTarget(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            IsPlayerNearby = false;
            ClearTarget();
        }
    }

    private void SetTarget(Player player)
    {
        DetectedPlayer = player;
    }

    private void ClearTarget() => DetectedPlayer = null;
}
