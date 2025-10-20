using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Cherry : MonoBehaviour
{
    public int CherryHeal { get; private set; }
    public Vector2 Position => transform.position;
    public bool IsActive;

    private void Awake()
    {
        CherryHeal = 10;
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void ResetCherry() => IsActive = true;

    public void CollectCherry() => IsActive = false;
}
