using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static InventoryHelper;

public class CraftItemDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject itemDisplay;
    [SerializeField] private float delayTime;

    private List<GameObject> hoverObjects;
    private ItemData item;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObjects = eventData.hovered;

        foreach (var Object in hoverObjects)
        {
            print(Object);
            if (Object.TryGetComponent<CraftingSlotUIButton>(out var itemData))
            {
                item = itemData.item;
                itemDisplay.transform.position = eventData.position;
                itemDisplay.transform.GetComponentInChildren<TextMeshProUGUI>(true).text = item.name;
                StartCoroutine(Delay());
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        itemDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDisplay.SetActive(false);
    }
}
