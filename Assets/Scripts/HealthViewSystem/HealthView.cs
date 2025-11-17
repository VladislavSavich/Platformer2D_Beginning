using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void OnEnable()
    {
        Health.Changed += UpdateView;
    }

    protected virtual void OnDisable() 
    {
        Health.Changed -= UpdateView;
    }

    protected abstract void UpdateView(int hitPoints, int maxhitPoints);
}
