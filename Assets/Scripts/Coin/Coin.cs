using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public Vector2 Position => transform.position;
    public event Action<Coin> CoinTaken;
    public bool IsActive { get; private set; }

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void ResetCoin() 
    {
        IsActive = true;
    }

    public void CollectCoin()
    {
        IsActive = false;
    }
}