using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    public static event Action<float> OnPlayerHit;
    [SerializeField] private GameObject _bulletExplosion;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Player")
        {
            OnPlayerHit?.Invoke(damage);
        }
        Instantiate(_bulletExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
