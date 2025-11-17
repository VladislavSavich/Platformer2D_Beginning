using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewSmoothSlider : HealthView
{
    [SerializeField] private Slider _smoothSlider;

    private float _stepValue = 10f;
    private Coroutine _smoothCoroutine;

    protected override void UpdateView(int hitPoints, int maxhitPoints)
    {
        if (_smoothSlider != null)
        {
            if (_smoothCoroutine != null)
                StopCoroutine(_smoothCoroutine);

            _smoothCoroutine = StartCoroutine(ChangeSliderValue(hitPoints));
        }
    }

    private IEnumerator ChangeSliderValue(int hp)
    {
        while (enabled)
        {
            _smoothSlider.value = Mathf.MoveTowards(_smoothSlider.value, hp, _stepValue * Time.deltaTime);

            yield return null;
        }
    }
}
