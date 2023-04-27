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

    private bool display;
    private static bool onlyDisplayOnce;
    private bool second;

    private void Start()
    {
        onlyDisplayOnce = true;
        second = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObjects = eventData.hovered;
        second = true;
        if(onlyDisplayOnce && second)
        {   
            onlyDisplayOnce = false;
            print(onlyDisplayOnce);
            foreach (var Object in hoverObjects)
            {
    
                if (Object.name.Equals("Item"))
                {
                    itemDisplay.transform.position = Object.transform.position + new Vector3(0f, -50f, 0f);
                    itemDisplay.transform.GetComponentInChildren<TextMeshProUGUI>(true).text = Object.GetComponent<Image>().sprite.name;
                    StartCoroutine(Delay());
                }
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
        onlyDisplayOnce = true;
        second = false;
    }
}
