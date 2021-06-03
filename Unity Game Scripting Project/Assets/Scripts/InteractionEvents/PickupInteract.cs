using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : MonoBehaviour
{
    public float scoreWorth = 3;

    public static event Action<float> OnPointsPickup;
    private Collider objectCollider;

    private void Awake()
    {
        InteractionEvents.OnObjectInteract += PickupItem;
    }

    private void OnDestroy()
    {
        InteractionEvents.OnObjectInteract -= PickupItem;
    }
    private void Start()
    {
        objectCollider = gameObject.GetComponent<Collider>();
    }

    private void PickupItem(Collider interactionCollider)
    {
        if (interactionCollider == objectCollider)
        {
        OnPointsPickup?.Invoke(scoreWorth);
            Destroy(gameObject);
        }
    }    
}
