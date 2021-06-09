using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageImpact : MonoBehaviour
{
    public int bulletDamage = 2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            /*ManageHealth.GetHit(bulletDamage); */           
        }
        Destroy(gameObject);
    }
}
