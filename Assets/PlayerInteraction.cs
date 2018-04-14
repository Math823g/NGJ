﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    private Camera mainCamera;
    public float distance;
    public GameObject old_gameobject;
    public GameObject current_gameobject;

    // Use this for initialization
    void Start () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	void FixedUpdate () {

        //Hovering
        Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, distance)){
            Debug.Log("NAME:" + hit.transform.gameObject.name);
            var interactableObject = hit.transform.gameObject.GetComponent<InteractableObject>();
            if(interactableObject != null){
                interactableObject.Hover();
            }
            current_gameobject = hit.transform.gameObject;
            old_gameobject = current_gameobject;
        }

        if(hit.transform == null) { current_gameobject = null; }

        if (old_gameobject != current_gameobject && old_gameobject != null)
        {
            var interactableObject = old_gameobject.GetComponent<InteractableObject>();
            if (interactableObject != null){
                interactableObject.NoHover();
            }
        }
    }
}
