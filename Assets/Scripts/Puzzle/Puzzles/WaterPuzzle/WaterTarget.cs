using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterTarget : MonoBehaviour
{
    public event Action OnTargetClicked;

    [SerializeField] Image targetImage;
    [SerializeField] float bottomY;
    [SerializeField] float topY;
    [SerializeField] float bobSpeed = 3;
    [SerializeField] AnimationCurve smoothFunc;

    Vector3 direction = new Vector3(1, 1, 0);

    public void SetSpeed(float speed)
    {
        direction.x = speed;
        direction.y = bobSpeed;
    }

    public void SetItem(Sprite image)
    {
        targetImage.sprite = image;
    }

    public void ClickTarget()
    {
        OnTargetClicked?.Invoke();
    }

    public void Update()
    {
        var yPos = transform.localPosition.y;

        if (yPos > topY || yPos < bottomY)
        {
            direction.y = -Mathf.Sign(direction.y) * bobSpeed;
        }

        if (transform.localPosition.x > Screen.width)
        {
            transform.localPosition = Vector2.left * Screen.width;
        }

        transform.position += smoothFunc.Evaluate((yPos - bottomY) / (topY - bottomY)) * Time.deltaTime * direction; 
    }
}
