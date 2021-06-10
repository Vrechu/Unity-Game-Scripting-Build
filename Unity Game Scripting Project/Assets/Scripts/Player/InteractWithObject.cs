using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour
{
    public Transform playerCamera;

    public float MaxInteractionDistance = 6;
    private bool imputE;
    public Text interactionUI;

    private void Start()
    {

    }

    private void Update()
    {
        imputE = Input.GetKeyDown(KeyCode.E);

        Interact();
    }

    private void Interact()
    {
        interactionUI.gameObject.SetActive(false);
        RaycastHit info;
        Physics.Raycast(playerCamera.position, playerCamera.forward, out info, MaxInteractionDistance);
        if (info.collider != null && info.collider.tag == "Interactable")
        {
            interactionUI.gameObject.SetActive(true);
            if (imputE)
            {
                InteractionEvents.ObjectInteract(info.collider);
            }
        }
    }
}