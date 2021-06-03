using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public Transform playerCamera;

    public float MaxInteractionDistance = 6;
    private bool rightClick;


    private void Start()
    {

    }

    private void Update()
    {
        rightClick = Input.GetKeyDown(KeyCode.E);

        Interact();
    }

    private void Interact()
    {
        if (rightClick)
        {
            RaycastHit info;
            Physics.Raycast(playerCamera.position, playerCamera.forward, out info, MaxInteractionDistance);

            if (info.collider != null)
            {
                InteractionEvents.ObjectInteract(info.collider);
            }
        }
    }
}