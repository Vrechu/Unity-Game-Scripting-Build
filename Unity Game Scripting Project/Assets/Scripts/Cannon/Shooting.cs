using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _barrel;

    [SerializeField] private float _fireRate = 5;
    [SerializeField] private float _resetTime;
    private bool _canShoot = false;
    private bool _spotted = false;
    
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
        _resetTime = _fireRate;
    }

    private void FixedUpdate()
    {
        if (_spotted
          && _canShoot)
        {
            Shoot();
        }
        FireTiming();
    }

    private void Shoot()
    {
        Vector3 shootLocation =_barrel.position + _barrel.forward / _barrel.localScale.y;
        Instantiate(_bullet, shootLocation, _barrel.rotation);
        _canShoot = false;
        OnGunshot?.Invoke(transform);
    }

    private void FireTiming()
    {
        if (!_canShoot)
        {            
            _resetTime -= Time.fixedDeltaTime;
            if (_resetTime < 0)
            {
                _resetTime = _fireRate;
                _canShoot = true;                               
            }
        }
    }

    private void StartShooting(Transform spotter)
    {
        if (_spotted = transform)
        {
            _resetTime = _fireRate;
            _spotted = true;
        }
    }
    private void StopShooting(Transform spotter)
    {
        if (_spotted = transform)
        {
            _spotted = false;
        }
    }
}
