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
    private bool canShoot = false;
    private bool spotted = false;
    
    public static event Action<Transform> OnGunshot;
    private void Awake()
    {
        SpotPlayer.OnPlayerSpotted += StartShooting;
        SpotPlayer.OnLostVisualOnPLayer += StopShooting;
    }

    private void OnDestroy()
    {
        SpotPlayer.OnPlayerSpotted -= StartShooting;
        SpotPlayer.OnLostVisualOnPLayer -= StopShooting;
    }


    private void Start()
    {     
        resetTime = fireRate;
    }

    private void FixedUpdate()
    {
        if (spotted
          && canShoot)
        {
            Shoot();
        }
        FireTiming();
    }

    private void Shoot()
    {
        Vector3 shootLocation =barrel.position + barrel.forward / barrel.localScale.y;
        Instantiate(bullet, shootLocation, barrel.rotation);
        canShoot = false;
        OnGunshot?.Invoke(transform);
    }

    private void FireTiming()
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

    private void StartShooting(Transform spotter)
    {
        if (spotted = transform)
        {
            resetTime = fireRate;
            spotted = true;
        }
    }
    private void StopShooting(Transform spotter)
    {
        if (spotted = transform)
        {
            spotted = false;
        }
    }
}
