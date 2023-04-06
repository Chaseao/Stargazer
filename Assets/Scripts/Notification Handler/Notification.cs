using UnityEngine;

public struct Notification
{
    private readonly Sprite image;
    private readonly string message;

    public string Message => message;
    public Sprite Image => image;

    public Notification(InventoryHelper.ItemData item)
    {
        image = item.itemImage;
        message = "Item Gained! \n" + item.name;
    }

    public Notification(Sprite image, string message)
    {
        this.image = image;
        this.message = message;
    }

    public Notification(ObjectiveData objective)
    {
        this.image = null;
        this.message = objective.Title;
    }
}
