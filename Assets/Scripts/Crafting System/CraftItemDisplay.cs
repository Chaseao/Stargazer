using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static InventoryHelper;

public class CraftItemDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject itemDisplay;
    [SerializeField] private float delayTime;

    private List<GameObject> hoverObjects;
    private ItemData item;

    private bool display;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObjects = eventData.hovered;

        foreach (var Object in hoverObjects)
        {
            print(Object);
            if (Object.name.Equals("Item"))
            {
                itemDisplay.transform.position = eventData.position;
                itemDisplay.transform.GetComponentInChildren<TextMeshProUGUI>(true).text = Object.GetComponent<Image>().sprite.name;
                display = true;
                StartCoroutine(Delay());
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        if(display) itemDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDisplay.SetActive(false);
        display= false;
    }
}
