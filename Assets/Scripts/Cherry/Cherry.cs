using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Cherry : MonoBehaviour
{
    public Vector2 Position => transform.position;
    public int CherryHeal { get; private set; }
    public bool IsActive { get; private set; }

    private void Awake()
    {
        CherryHeal = 10;
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void ResetCherry() => IsActive = true;

    public void CollectCherry() => IsActive = false;
}
