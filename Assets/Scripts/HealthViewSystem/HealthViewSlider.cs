using UnityEngine;
using UnityEngine.UI;

public class HealthViewSlider : HealthView
{
    [SerializeField] private Slider _slider;

    protected override void UpdateView(int hitPoints, int maxhitPoints)
    {
        if (_slider != null)
            _slider.value = hitPoints;
    }
}
