using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;    
    public Transform barrel;

    public float fireRate = 5;
    float resetTime;
    bool canShoot = false;
    bool spotted = false;
    
    public static event Action<Transform> OnGunshot;
    void Awake()
    {
        SpotPlayer.OnPlayerSpotted += StartShooting;
        SpotPlayer.OnLostVisualOnPLayer += StopShooting;
    }

    void OnDestroy()
    {
        SpotPlayer.OnPlayerSpotted -= StartShooting;
        SpotPlayer.OnLostVisualOnPLayer -= StopShooting;
    }


    void Start()
    {     
        resetTime = fireRate;
    }

    void FixedUpdate()
    {
        if (spotted
          && canShoot)
        {
            Shoot();
        }
        FireTiming();
    }

    void Shoot()
    {
        Vector3 shootLocation =barrel.position + barrel.forward / barrel.localScale.y;
        Instantiate(bullet, shootLocation, barrel.rotation);
        canShoot = false;
        OnGunshot?.Invoke(transform);
    }

    void FireTiming()
    {
        if (!canShoot)
        {            
            resetTime -= Time.fixedDeltaTime;
            if (resetTime < 0)
            {
                resetTime = fireRate;
                canShoot = true;                               
            }
        }
    }

    void StartShooting(Transform spotter)
    {
        if (spotted = transform)
        {
            resetTime = fireRate;
            spotted = true;
        }
    }
    void StopShooting(Transform spotter)
    {
        if (spotted = transform)
        {
            spotted = false;
        }
    }
}
