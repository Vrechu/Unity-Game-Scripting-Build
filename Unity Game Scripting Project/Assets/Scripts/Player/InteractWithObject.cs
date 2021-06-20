using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;

    [SerializeField] private float _maxInteractionDistance = 6;
    private bool _imputE;
    [SerializeField] private Text _interactionUI;


    private void Update()
    {
        _imputE = Input.GetKeyDown(KeyCode.E);
        Interact();
    }

    /// <summary>
    /// sends out the on object interact event when pressing imput while facing an interactable object.
    /// </summary>
    private void Interact()
    {
        _interactionUI.gameObject.SetActive(false);
        RaycastHit info;
        Physics.Raycast(_playerCamera.position, _playerCamera.forward, out info, _maxInteractionDistance);
        if (info.collider != null && info.collider.tag is "Interactable")
        {
            _interactionUI.gameObject.SetActive(true);
            if (_imputE)
            {
                InteractionEvents.ObjectInteract(info.collider);
            }
        }
    }
}