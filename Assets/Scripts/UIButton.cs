using System;
using UnityEngine;

public abstract class UIButton : MonoBehaviour, IButton
{
    public event Action<IButton> OnClick;

    public void Click()
    {
        OnClick?.Invoke(this);
        Use();
    }

    public virtual void ToggleSelected(bool isSelected)
    {
        transform.localScale = Vector3.one * (isSelected ? 1.2f : 1);
    }

    public abstract void Use();
}