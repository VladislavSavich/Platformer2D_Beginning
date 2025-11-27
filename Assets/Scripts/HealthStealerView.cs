using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthStealerView : MonoBehaviour
{
    [SerializeField] private HealthStealer _healthStealer;
    [SerializeField] private Image _smoothSlider;
    [SerializeField] private Image _spriteRadius;

    private float _stepValue = 0.5f;
    private float _targetFillAmount;
    private Coroutine _smoothCoroutine;

    private void OnEnable()
    {
        _healthStealer.Changed += UpdateView;
    }

    private void OnDisable()
    {
        _healthStealer.Changed -= UpdateView;
    }

    private void UpdateView(float points, float maxPoints)
    {
        _targetFillAmount = points / maxPoints;

        if (_smoothSlider != null)
        {
            if (_smoothCoroutine != null)
                StopCoroutine(_smoothCoroutine);

            _smoothCoroutine = StartCoroutine(ChangeSliderValue(_targetFillAmount));
        }

        ChangeSpriteState();
    }

    private void ChangeSpriteState()
    {
        if (_spriteRadius != null)
        {
            if (_healthStealer.VampirismAvtivated)
                _spriteRadius.enabled = true;
            else
                _spriteRadius.enabled = false;
        }
    }

    private IEnumerator ChangeSliderValue(float count)
    {
        while (enabled)
        {
            _smoothSlider.fillAmount = Mathf.MoveTowards(_smoothSlider.fillAmount, count, _stepValue * Time.deltaTime);

            yield return null;
        }
    }
}
