using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI textField;


    public void Initialize(Notification notification)
    {
        SetNotification(notification);
    }

    private void SetNotification(Notification notification)
    {
        image.sprite = notification.Image;
        image.enabled = image.sprite != null;

        textField.text = notification.Message;
    }

    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
