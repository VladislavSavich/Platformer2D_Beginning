using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Health _hitPoints;
    [SerializeField] private float _skillDuration = 6;
    [SerializeField] private float _skillCooldown = 4;
    [SerializeField] private float _skillRadius = 5;

    private WaitForSeconds _vampirismWait = new WaitForSeconds(1f);
    private int _damageDivider = 2;
    private float _timer;
    private bool _canSteal = true;

    public event Action<float, float> Changed;

    public bool IsVampirismActive { get; private set; }

    public void StealHealth()
    {
        if (_canSteal)
            StartCoroutine(PerformVampirism());
    }

    private IEnumerator PerformVampirism()
    {
        _timer = _skillDuration;
        _canSteal = false;
        IsVampirismActive = true;

        while (_timer >= 0)
        {
            Enemy nearestEnemy = FindNearestEnemy();

            if (nearestEnemy != null)
            {
                _attacker.DealDamage(nearestEnemy.gameObject);
                _hitPoints.TakeHeal(_attacker.Damage / _damageDivider);
            }

            yield return _vampirismWait;

            _timer -= 1f;
            Changed?.Invoke(_timer, _skillDuration);
        }

        IsVampirismActive = false;
        StartCoroutine(StartSkillCooldown());
    }

    private Enemy FindNearestEnemy() 
    {
        Collider2D[] allEnemiesInRadius = Physics2D.OverlapCircleAll(transform.position, _skillRadius);
        Enemy nearestEnemy = null;
        float nearestSqrDistance = float.MaxValue;

        foreach (Collider2D enemyInRadius in allEnemiesInRadius) 
        {
            if (enemyInRadius.TryGetComponent<Enemy>(out Enemy enemy)) 
            {
                float sqrDistance = (enemy.transform.position - transform.position).sqrMagnitude;

                if (sqrDistance < nearestSqrDistance) 
                {
                    nearestSqrDistance = sqrDistance;
                    nearestEnemy = enemy;              
                }
            }       
        }

        return nearestEnemy;   
    }

    private IEnumerator StartSkillCooldown()
    {
        _timer = 0;

        while (_timer <= _skillCooldown)
        {
            yield return _vampirismWait;

            _timer += 1f;
            Changed?.Invoke(_timer, _skillCooldown);
        }

        _canSteal = true;
    }
}
