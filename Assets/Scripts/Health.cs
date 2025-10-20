using UnityEngine;

public class Health : MonoBehaviour
{
    private int _hitPoints = 100;
    private int _maxHitPoints = 100;

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;
    }

    public void TakeHeal(int health)
    {
        _hitPoints += health;

        if (_hitPoints > _maxHitPoints)
            _hitPoints = _maxHitPoints;
    }
}
