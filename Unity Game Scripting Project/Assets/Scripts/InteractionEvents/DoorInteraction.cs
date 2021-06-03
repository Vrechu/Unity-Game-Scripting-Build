using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public static event Action<string> OnDoorInteract;
    public string nextSceneName;
    private Collider objectCollider;

    private void Awake()
    {
        InteractionEvents.OnObjectInteract += UseDoor;
    }

    private void OnDestroy()
    {
        InteractionEvents.OnObjectInteract -= UseDoor;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = gameObject.GetComponent<Collider>();
    }

    private void UseDoor(Collider interactionCollider)
    {
        if (interactionCollider == objectCollider)
        {
            OnDoorInteract?.Invoke(nextSceneName);
        }
    }
}
