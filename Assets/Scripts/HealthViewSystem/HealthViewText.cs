using TMPro;
using UnityEngine;

public class HealthViewText : HealthView
{
    [SerializeField] private TextMeshProUGUI _text;

    protected override void UpdateView(int hitPoints, int maxhitPoints)
    {
        if (_text != null)
            _text.text = $"{hitPoints}/{maxhitPoints}";
    }
}
