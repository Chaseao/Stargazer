using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CursorController : MonoBehaviour
{
    [SerializeField] private InteractablesManager interactablesManager;

    [SerializeField] private Transform newSelectionTransform;
    private Transform currentSelectionTransform;

    [SerializeField] private float distanceThreshold;

    private Vector2 inputPositionVector;

    [SerializeField] private AudioSource interactSound;

    private void OnEnable()
    {
        Controller.OnPosition += Position;
        Controller.OnClick += Click;
    }

    private void OnDisable()
    {
        Controller.OnPosition -= Position;
        Controller.OnClick -= Click;
    }

    void Update()
    {
        FindInteractableWithinDistanceThreshold();
    }

    private void FindInteractableWithinDistanceThreshold()
    {
        newSelectionTransform =null;

        for(int itemIndex=0; itemIndex<interactablesManager.Interactables.Count; itemIndex++)
        {
            Vector2 fromMouseToInteractableOffset = interactablesManager.Interactables[itemIndex].position - new Vector3(inputPositionVector.x, inputPositionVector.y, 0f);
            if(fromMouseToInteractableOffset.sqrMagnitude < distanceThreshold * distanceThreshold) 
            {
                //Found an interable exit out of loop
                newSelectionTransform = interactablesManager.Interactables[itemIndex].transform;
                break;
            }        
        }

        if(currentSelectionTransform != newSelectionTransform)
        {
            //Make CursorDefault no interactable found
            currentSelectionTransform = newSelectionTransform;
        }
    }

    void Position(Vector2 input)
    {
        inputPositionVector = input;
    }

    void Click()
    {
        OnClickInteractable();
    }

    private void OnClickInteractable()
    {
        if(newSelectionTransform != null)
        {
            IInteractable interactable = newSelectionTransform.gameObject.GetComponent<IInteractable>();
            if(interactable != null)
            {
                if (interactable.ExecuteDialogue()) interactSound.Play();
                interactable.OpenDoor();
            }
            newSelectionTransform = null;
        }
    }
}
