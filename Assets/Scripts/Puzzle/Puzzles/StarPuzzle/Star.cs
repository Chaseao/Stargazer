using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Star : MonoBehaviour, IPointerDownHandler
{
    private bool pressed = false;
    private bool placed = false;
    private Vector2 orginialPosition;
    [SerializeField] private StarPuzzle starPuzzle;

    private List<GameObject> hoverObjects;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!placed)
        {
            orginialPosition = this.transform.position;
            pressed = true;
            StartCoroutine(FollowCursor());
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed= false;
        hoverObjects = eventData.hovered;
    }


    private IEnumerator FollowCursor()
    {
        yield return null;
        this.transform.position = starPuzzle.inputPositionVector;
        if(!pressed)
        {
            CheckForStar();
        }
    }
    private void CheckForStar()
    {
        foreach (GameObject Object in hoverObjects)
        {
            if (Object.name.Equals(this.name + "Place"))
            {
                 PlaceStar(Object);
            }
            else
            {
                ReturnStar();
            }
        }
    }
    
    private void PlaceStar(GameObject Object)
    {
        this.transform.position = Object.transform.position;
        starPuzzle.StarFinished++; placed= true;
    }

    private void ReturnStar()
    {
        this.transform.position = orginialPosition;
    }
}
