using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewSmoothSlider : HealthView
{
    [SerializeField] private Image _smoothSlider;

    private float _stepValue = 0.1f;
    private float _targetFillAmount;
    private Coroutine _smoothCoroutine;

    protected override void UpdateView(int hitPoints, int maxhitPoints)
    {
        _targetFillAmount = (float)hitPoints / maxhitPoints;

        if (_smoothSlider != null)
        {
            if (_smoothCoroutine != null)
                StopCoroutine(_smoothCoroutine);

            _smoothCoroutine = StartCoroutine(ChangeSliderValue(_targetFillAmount));
        }
    }

    private IEnumerator ChangeSliderValue(float hp)
    {
        while (enabled)
        {
            _smoothSlider.fillAmount = Mathf.MoveTowards(_smoothSlider.fillAmount, hp, _stepValue * Time.deltaTime);

            yield return null;
        }
    }
}
