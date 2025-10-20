using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Vector2[] _spawnPoints;
    [SerializeField] private Collector _collector;

    private Dictionary<Vector2, Coin> _activeCoins = new Dictionary<Vector2, Coin>();
    private ObjectPool<Coin> _pool;
    private int _poolCapacity = 3;
    private int _poolMaxSize = 3;
    private float _respawnTime = 3f;
    public event Action CoinRelease;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
        createFunc: () => Instantiate(_coinPrefab),
        actionOnGet: (coin) => ActionOnGet(coin),
        actionOnRelease: (coin) => coin.gameObject.SetActive(false),
        actionOnDestroy: (coin) => Destroy(coin),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Coin coin)
    {
        coin.gameObject.SetActive(true);
        coin.ResetCoin();
        _collector.CoinTaken += ReleaseCoin;
    }

    private void Start()
    {
        SpawnAllCoins();
    }

    private void SpawnAllCoins()
    {
        foreach (Vector2 spawnPoint in _spawnPoints)
        {
            SpawnCoinAt(spawnPoint);
        }
    }

    private void SpawnCoinAt(Vector2 position)
    {
        Coin coin = _pool.Get();
        coin.transform.position = position;

        _activeCoins[position] = coin;
    }

    private IEnumerator RespawnCoin(Vector2 position)
    {
        yield return new WaitForSeconds(_respawnTime);

        SpawnCoinAt(position);
    }

    private void ReleaseCoin(Coin coin)
    {
        if (_activeCoins.ContainsKey(coin.Position))
        {
            _collector.CoinTaken -= ReleaseCoin;
            _activeCoins.Remove(coin.Position);
            _pool.Release(coin);
            CoinRelease?.Invoke();
            StartCoroutine(RespawnCoin(coin.Position));
        }
    }
}