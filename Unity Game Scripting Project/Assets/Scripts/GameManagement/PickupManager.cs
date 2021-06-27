using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager GetPickupManager()
    {
        return PickupManagerSingleton;
    }
    static PickupManager PickupManagerSingleton = null;

    [SerializeField] private bool _canSprint = false;
    public bool CanSprint()
    {
        return _canSprint;
    }

    private void Awake()
    {
        if (PickupManagerSingleton == null) PickupManagerSingleton = this;
        PickupInteract.OnBootsPickup += PickupBoots;
    }

    private void OnDestroy()
    {
        PickupInteract.OnBootsPickup -= PickupBoots;
    }

    private void PickupBoots()
    {
        _canSprint = true;
    }
}
