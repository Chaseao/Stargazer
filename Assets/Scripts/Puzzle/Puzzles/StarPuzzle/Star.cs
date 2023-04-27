using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Star : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pressed = true;
    private bool placed = false;
    private Vector2 orginialPosition;
    [SerializeField] private StarPuzzle starPuzzle;

    private List<GameObject> hoverObjects;

    private void Start()
    {
        orginialPosition = this.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!placed)
        {
            pressed = true;
            StartCoroutine(FollowCursor());
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        hoverObjects = eventData.hovered;
    }


    private IEnumerator FollowCursor()
    {
        while(pressed)
        {
            yield return null;
            this.transform.position = starPuzzle.inputPositionVector;
        }
        CheckForStar(); 

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
        starPuzzle.StarFinished++; 
        placed= true;
    }

    private void ReturnStar()
    {
        this.transform.position = orginialPosition;
    }

    private void Update()
    {
        if(starPuzzle.StarFinished ==0 && starPuzzle.inStarPuzzle)
        {
           this.transform.position = orginialPosition;
        }
        if(starPuzzle.StarFinished >= 7)
        {
            pressed = true;
            placed = false;
        }
    }
}
