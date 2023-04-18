using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static InventoryHelper;

public class SelectionItemDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject itemDisplay;
    [SerializeField] private float delayTime;

    private List<GameObject> hoverObjects;

    private bool display;
    private static bool onlyDisplayOnce;

    private void Start()
    {
        onlyDisplayOnce = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObjects = eventData.hovered;
        if (onlyDisplayOnce)
        {
            onlyDisplayOnce = false;
            print(onlyDisplayOnce);
            foreach (var Object in hoverObjects)
            {

                if (Object.name.Equals("Item"))
                {
                    itemDisplay.transform.position = Object.transform.position + new Vector3(0f, -5f, 0f);
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
    }
}

