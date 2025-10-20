using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CherrySpawner : MonoBehaviour
{
    [SerializeField] private Cherry _cherryPrefab;
    [SerializeField] private Collector _collector;
    [SerializeField] private LayerMask _groundLayer;

    private Dictionary<Vector2, Cherry> _activeCherry = new Dictionary<Vector2, Cherry>();
    private Vector2 _minimumSpawnPoint = new Vector2(-12, -4);
    private Vector2 _maximumSpawnPoint = new Vector2(8, 4);
    private ObjectPool<Cherry> _pool;
    private int _poolCapacity = 1;
    private int _poolMaxSize = 1;
    private float _respawnTime = 3f;
    public event Action CherryRelease;

    private void Awake()
    {
        _pool = new ObjectPool<Cherry>(
        createFunc: () => Instantiate(_cherryPrefab),
        actionOnGet: (cherry) => ActionOnGet(cherry),
        actionOnRelease: (cherry) => cherry.gameObject.SetActive(false),
        actionOnDestroy: (cherry) => Destroy(cherry),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cherry cherry)
    {
        cherry.gameObject.SetActive(true);
        cherry.ResetCherry();
        _collector.CherryTaken += ReleaseCherry;
    }

    private void Start()
    {
        SpawnAllCherrys();
    }

    private void SpawnAllCherrys()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            SpawnCherry();
        }
    }

    private void SpawnCherry()
    {
        Cherry cherry = _pool.Get();
        Vector2 position = GenerateRandomPosition();

        while (!CanSpawnAtPosition(position)) 
        {
            position = GenerateRandomPosition();
        }

        cherry.transform.position = position;
        _activeCherry[position] = cherry;
    }

    private IEnumerator RespawnCherry()
    {
        yield return new WaitForSeconds(_respawnTime);

        SpawnCherry();
    }

    private void ReleaseCherry(Cherry cherry)
    {
        if (_activeCherry.ContainsKey(cherry.Position))
        {
            _collector.CherryTaken -= ReleaseCherry;
            _activeCherry.Remove(cherry.Position);
            _pool.Release(cherry);
            CherryRelease?.Invoke();
            StartCoroutine(RespawnCherry());
        }
    }

    private Vector2 GenerateRandomPosition() 
    {
        float x = Random.Range(_minimumSpawnPoint.x, _maximumSpawnPoint.x);
        float y = Random.Range(_minimumSpawnPoint.y, _maximumSpawnPoint.y);

        return new Vector2(x, y);
    }

    private bool CanSpawnAtPosition(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, 0.5f, _groundLayer);

        return hit == null;
    }
}
