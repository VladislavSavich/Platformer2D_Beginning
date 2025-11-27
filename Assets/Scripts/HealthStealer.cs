using System;
using System.Collections;
using UnityEngine;

public class HealthStealer : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Health _hitPoints;
    [SerializeField] private float _skillDuration = 6;
    [SerializeField] private float _skillCooldown = 4;
    [SerializeField] private float _skillRadius = 5;

    private int _damageDivider = 2;
    private float _timer;
    private bool _canSteal = true;

    public event Action<float, float> Changed;

    public bool VampirismAvtivated { get; private set; }

    public void StealHealth()
    {
        if (_canSteal)
            StartCoroutine(Vampirism());
    }

    private IEnumerator Vampirism()
    {
        _timer = _skillDuration;
        _canSteal = false;
        VampirismAvtivated = true;

        while (_timer >= 0)
        {
            Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, _skillRadius);

            foreach (Collider2D enemyCollider in enemiesInRadius)
            {
                if (enemyCollider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    _attacker.DealDamage(enemy.gameObject);
                    _hitPoints.TakeHeal(_attacker.Damage / _damageDivider);
                    break;
                }
            }

            yield return new WaitForSeconds(1f);

            _timer -= 1f;
            Changed?.Invoke(_timer, _skillDuration);
        }

        VampirismAvtivated = false;
        StartCoroutine(SkillCooldown());
    }

    private IEnumerator SkillCooldown()
    {
        _timer = 0;

        while (_timer <= _skillCooldown)
        {
            yield return new WaitForSeconds(1f);

            _timer += 1f;
            Changed?.Invoke(_timer, _skillCooldown);
        }

        _canSteal = true;
    }
}
