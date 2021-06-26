using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _bulletSpeed = 20;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _bulletSpeed, ForceMode.VelocityChange);
    }
}
