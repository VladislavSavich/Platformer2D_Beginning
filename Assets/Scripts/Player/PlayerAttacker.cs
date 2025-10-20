using System.Collections;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private int _damage = 10;
    private float _attackCooldown = 2f;
    private bool _canAttack = true;

    public void DealDamage(Enemy enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();

        if (enemyHealth != null && _canAttack)
        {
            enemyHealth.TakeDamage(_damage);
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
