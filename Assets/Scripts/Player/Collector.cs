using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action<Coin> CoinTaken;
    public event Action<Cherry> CherryTaken;
    public event Action<int> HealTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin) && coin.IsActive)
        {
            coin.CollectCoin();
            CoinTaken?.Invoke(coin);
        }
        else if (collision.gameObject.TryGetComponent(out Cherry cherry) && cherry.IsActive) 
        {
            cherry.CollectCherry();
            CherryTaken?.Invoke(cherry);
            HealTaken?.Invoke(cherry.CherryHeal);
        }
    }
}
