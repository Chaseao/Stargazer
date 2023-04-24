﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public abstract class UIButton : MonoBehaviour, IButton, IPointerEnterHandler
{
    public event Action<IButton> OnSelect;
    public event Action<IButton> OnClick;
    protected bool isSelected;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected) OnSelect?.Invoke(this);
    }

    public void Click()
    {
        OnClick?.Invoke(this);
        Use();
    }

    public virtual void ToggleSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        transform.localScale = Vector3.one * (isSelected ? 1.2f : 1);
    }

    public abstract void Use();
}
