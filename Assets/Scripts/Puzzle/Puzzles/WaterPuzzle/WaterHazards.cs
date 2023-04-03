using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterHazards : MonoBehaviour
{
    public event Action OnHazardClicked;

    [SerializeField] float totalTime;
    [SerializeField] Image flash;
    [SerializeField] AnimationCurve flashPattern;

    private void Awake()
    {
        flash.enabled = false;
    }

    public void ClickHazard()
    {
        OnHazardClicked?.Invoke();
        StopCoroutine(nameof(ActivateHazardBlink));
        StartCoroutine(nameof(ActivateHazardBlink));
    }

    private IEnumerator ActivateHazardBlink()
    {
        float startTime = Time.time;
        flash.enabled = true;

        while(Time.time - startTime < totalTime)
        {
            Color temp = flash.color;
            temp.a = flashPattern.Evaluate((Time.time - startTime) / totalTime);
            flash.color = temp;
            yield return null;
        }

        flash.enabled = false;
    }
}