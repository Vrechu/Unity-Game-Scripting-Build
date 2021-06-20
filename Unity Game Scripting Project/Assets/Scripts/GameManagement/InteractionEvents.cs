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
}
