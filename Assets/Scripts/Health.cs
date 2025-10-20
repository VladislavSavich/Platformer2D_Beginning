using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _hitPoints = 100;

    private int _maxHitPoints = 100;

    public void TakeDamage(int damage)
    {
        if(damage > 0)
            _hitPoints -= damage;
    }

    public void TakeHeal(int health)
    {
        if(health > 0)
            _hitPoints += health;

        if (_hitPoints > _maxHitPoints)
            _hitPoints = _maxHitPoints;
    }
}
