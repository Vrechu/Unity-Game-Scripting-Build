using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    public static event Action<float> OnPlayerHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Player")
        {
            OnPlayerHit?.Invoke(damage);        
        }
        Destroy(gameObject);
    }
}
