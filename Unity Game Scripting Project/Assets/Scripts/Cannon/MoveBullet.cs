﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed = 20;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }
}
