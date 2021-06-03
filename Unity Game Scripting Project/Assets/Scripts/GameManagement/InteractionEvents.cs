using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvents : MonoBehaviour
{
    public static event Action<Collider> OnObjectInteract;


    public static void ObjectInteract(Collider objectCollider)
    {
        OnObjectInteract?.Invoke(objectCollider);
    }

    private void Awake()
    {
        OnObjectInteract += PrintObjectTag;
    }

   private  void OnDestroy()
    {
        OnObjectInteract -= PrintObjectTag;
    }

    private void PrintObjectTag(Collider collider)
    {
        Debug.Log(collider.tag);
    }
}
