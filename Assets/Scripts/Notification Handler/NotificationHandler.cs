using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    [SerializeField] NotificationPopup notificationPopupPrefab;

    List<NotificationPopup> popups = new List<NotificationPopup>();

    public static void CreateNotification(string message, Sprite image)
    {
        Notification notification = new(image, message);
        FindObjectOfType<NotificationHandler>().CreateNotification(notification);
    }

    private void Start()
    {
        InventoryManager.OnItemGained += OnItemGained;
    }

    private void OnItemGained(InventoryHelper.ItemData item)
    {
        CreateNotification(new Notification(item));
    }

    private void CreateNotification(Notification notification)
    {
        NotificationPopup popup = Instantiate(notificationPopupPrefab);
        popup.Initialize(notification);
        popup.transform.SetParent(transform);

        popups.RemoveAll(n => n == null);
        if(popups.Count > 2)
        {
            popups[0].DestroyPopup();
            popups.RemoveAt(0);
        }
        popups.Add(popup);
    }

    private void OnDestroy()
    {
        InventoryManager.OnItemGained -= OnItemGained;
    }
}
