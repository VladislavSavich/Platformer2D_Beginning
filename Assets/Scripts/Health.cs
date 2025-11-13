using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healPoints;
    [SerializeField] private Slider _healSlider;
    [SerializeField] private Slider _smoothHealSlider;
    [SerializeField] private int _hitPoints = 100;
    [SerializeField] private int _maxHitPoints = 100;

    private float _stepValue = 0.05f;

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _hitPoints -= damage;
            UpdateHealthView();
        }
    }

    public void TakeHeal(int health)
    {
        if (health > 0)
            _hitPoints += health;

        if (_hitPoints > _maxHitPoints)
            _hitPoints = _maxHitPoints;

        UpdateHealthView();
    }

    private void UpdateHealthView()
    {
        if (_healPoints && _healSlider && _smoothHealSlider != null)
        {
            _healPoints.text = $"{_hitPoints}/{_maxHitPoints}";
            _healSlider.value = _hitPoints;
            StartCoroutine(ChangeSliderValue());
        }
    }

    private IEnumerator ChangeSliderValue()
    {
        while (enabled) 
        {
            _smoothHealSlider.value = Mathf.MoveTowards(_smoothHealSlider.value, _hitPoints, _stepValue);

            yield return null;
        }
    }
}
