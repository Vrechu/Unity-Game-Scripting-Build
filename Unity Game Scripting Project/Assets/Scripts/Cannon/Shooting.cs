using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _barrel;

    [SerializeField] private float _fireRate = 5;
    [SerializeField] private float _counter;
    
    public event Action<Transform> OnGunshot;

    private SpotPlayer _spotPlayer;

    private void Awake()
    {
        _spotPlayer = GetComponent<SpotPlayer>();
    }

    private void Start()
    {     
        _counter = _fireRate;
    }

    private void Update()
    {
        switch (_spotPlayer.GetCannonState())
        {
            case SpotPlayer.CannonState.SPOTTED:
                {
                    if (CanShoot()) Shoot();
                    if (_counter > -1) _counter -= Time.deltaTime;
                    break;
                }
            case SpotPlayer.CannonState.LOSTVISUAL:
                {
                    if (_counter > -1) _counter -= Time.deltaTime;
                    break;
                }
            case SpotPlayer.CannonState.UNSPOTTED:
                {
                    _counter = _fireRate;
                    break;
                }
        }
    }

    /// <summary>
    /// shoots a bullet in the direction of the barrel.
    /// resets the shooting counter
    /// </summary>
    private void Shoot()
    {
        Vector3 shootLocation =_barrel.position + _barrel.forward / _barrel.localScale.y;
        Instantiate(_bullet, shootLocation, _barrel.rotation);
        OnGunshot?.Invoke(transform);
        _counter = _fireRate;
    }

    /// <summary>
    /// returns true if the counter is below 0;
    /// </summary>
    /// <returns></returns>
    private bool CanShoot()
    {
        if (_counter < 0)
        {            
            return true;
        }
        else return false;
    }
}
