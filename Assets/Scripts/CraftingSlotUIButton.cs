using UnityEngine;

public class CraftingSlotUIButton : UIButton
{
    [SerializeField] string item;
    
    public void SetItem(string item)
    {
        this.item = item;
    }

    public override void Use()
    {
        Debug.Log("Selected: " + item);
    }
}
