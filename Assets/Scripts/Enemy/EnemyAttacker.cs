using System.Collections;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private int _damage = 10;
    private float _attackCooldown = 2f;
    private bool _canAttack = true;

    public void DealDamage(Player player)
    {
        Health playerHealth = player.GetComponent<Health>();

        if (playerHealth != null && _canAttack)
        {
            playerHealth.TakeDamage(_damage);
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
