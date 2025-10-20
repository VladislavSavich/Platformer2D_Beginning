using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 2f;

    private bool _canAttack = true;

    public void DealDamage(GameObject target)
    {
        Health targetHealth = null;
        
        if (target.TryGetComponent<Health>(out Health health))
            targetHealth = health;

        if (targetHealth != null && _canAttack)
        {
            targetHealth.TakeDamage(_damage);
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
