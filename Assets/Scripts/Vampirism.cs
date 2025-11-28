using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private NearestEnemySearcher _enemySearcher;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Health _hitPoints;
    [SerializeField] private float _skillDuration = 6;
    [SerializeField] private float _skillCooldown = 4;

    private WaitForSeconds _vampirismWait;
    private float _timerStep = 1f;
    private int _damageDivider = 2;
    private float _timer;
    private bool _canSteal = true;

    public event Action<float, float> Changed;

    public bool IsVampirismActive { get; private set; }

    private void Start()
    {
        _vampirismWait = new WaitForSeconds(_timerStep);
    }

    private IEnumerator PerformVampirism()
    {
        _timer = _skillDuration;
        _canSteal = false;
        IsVampirismActive = true;

        while (_timer >= 0)
        {
            Enemy nearestEnemy = _enemySearcher.FindNearestEnemy();

            if (nearestEnemy != null)
            {
                _attacker.DealDamage(nearestEnemy.gameObject);
                _hitPoints.TakeHeal(_attacker.Damage / _damageDivider);
            }

            yield return _vampirismWait;

            _timer -= _timerStep;
            Changed?.Invoke(_timer, _skillDuration);
        }

        IsVampirismActive = false;
        StartCoroutine(StartSkillCooldown());
    }

    private IEnumerator StartSkillCooldown()
    {
        _timer = 0;

        while (_timer <= _skillCooldown)
        {
            yield return _vampirismWait;

            _timer += _timerStep;
            Changed?.Invoke(_timer, _skillCooldown);
        }

        _canSteal = true;
    }

    public void StealHealth()
    {
        if (_canSteal)
            StartCoroutine(PerformVampirism());
    }
}
