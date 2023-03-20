using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractablesManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Transform> interactables;

    public List<Transform> Interactables
    {
        get => interactables;
    }

    // Start is called before the first frame update
    void Start()
    {
        AllChildrenWorldToScreenPoint();
    }

    private void AllChildrenWorldToScreenPoint()
    {
        for(int i=0;i<transform.childCount; i++)
        {
            transform.GetChild(i).position = mainCamera.WorldToScreenPoint(transform.GetChild(i).position);
            //transform.GetChild(i).localScale = Vector2.one * 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
