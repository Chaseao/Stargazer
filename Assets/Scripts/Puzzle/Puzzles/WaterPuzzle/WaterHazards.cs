using System;
using UnityEngine;

public class WaterHazards : MonoBehaviour
{
    public event Action OnHazardClicked;

    public void ClickHazard()
    {
        OnHazardClicked?.Invoke();
    }
}