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
    private GameObject craftItem;
    private ItemData item;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObjects = eventData.hovered;
        foreach (var Object in hoverObjects)
        {
            if(Object.GetComponent<CraftingSlotUIButton>() != null)
            {
                craftItem = Object;
            }
        }
        item = craftItem.GetComponent<CraftingSlotUIButton>().item;
        itemDisplay.transform.position = eventData.position;
        itemDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
        StartCoroutine(Delay());
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
