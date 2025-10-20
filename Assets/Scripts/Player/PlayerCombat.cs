using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int _health = 100;
    private int _maxHealth = 100;

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public void TakeHeal(int health)
    {
        _health += health;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }
}
