using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDisplayToggler : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        canvas.enabled = Controller.Instance.InGameplay;
    }
}
