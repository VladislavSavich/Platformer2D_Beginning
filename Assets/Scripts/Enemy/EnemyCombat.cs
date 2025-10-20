using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private int _damage = 10;
    private float _attackCooldown = 2f;
    private bool _canAttack = true;

    public void DealDamage(Player player) 
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        if (playerCombat != null && _canAttack) 
        {
            playerCombat.TakeDamage(_damage);
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
