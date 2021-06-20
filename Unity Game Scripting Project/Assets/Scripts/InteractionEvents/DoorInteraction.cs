using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public static event Action<string> OnDoorInteract;
    [SerializeField] private string nextSceneName;
    private Collider objectCollider;

    private void Awake()
    {
        InteractionEvents.OnObjectInteract += UseDoor;
    }

    private void OnDestroy()
    {
        InteractionEvents.OnObjectInteract -= UseDoor;
    }

    void Start()
    {
        objectCollider = gameObject.GetComponent<Collider>();
    }

    /// <summary>
    /// if the interacted object is this object, snd out the on door interact event.
    /// </summary>
    /// <param name="interactionCollider">the interacted object collider</param>
    private void UseDoor(Collider interactionCollider)
    {
        if (interactionCollider == objectCollider)
        {
            OnDoorInteract?.Invoke(nextSceneName);
        }
    }
}
