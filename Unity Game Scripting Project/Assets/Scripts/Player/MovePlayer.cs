using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _baseAcceleration = 5;
    [SerializeField] private float _baseTopSpeed = 10;
    [SerializeField] private float _sprintAcceleration = 10;
    [SerializeField] private float _sprintTopSpeed = 20;

    [SerializeField] private float _inertia = 0.05f;
    private float _acceleration;
    private float _topSpeed;



    private PickupManager _pickupManager;
    private float _sprint;


    [SerializeField] private float _horizontalCameraSensitivity = 10;

    private float _forward;
    private float _sideways;

    private Rigidbody _rb;

    private void Start()
    {
        _pickupManager = PickupManager.GetPickupManager();
        _rb = GetComponent<Rigidbody>();
        _acceleration = _baseAcceleration;
    }

    private void FixedUpdate()
    {
        MoveHorizontally();
    }

    private void Update()
    {
        SetKeys();
        Sprint();
        transform.Rotate(0, Input.GetAxis("Mouse X") * _horizontalCameraSensitivity, 0);        
    }

    /// <summary>
    /// sets the horizontal imput keys
    /// </summary>
    private void SetKeys()
    {
        _forward = Input.GetAxis("Vertical");
        _sideways = Input.GetAxis("Horizontal");

        _sprint = Input.GetAxis("Fire3");
    }

    /// <summary>
    /// sets the player velocity
    /// </summary>
    private void MoveHorizontally()
    {
        Vector3 zVelocity = transform.forward * _forward;
        Vector3 xVelocity = transform.right * _sideways;
        Vector3 horizontalVelocity = (zVelocity + xVelocity).normalized * _acceleration;
        if (_rb.velocity.magnitude < _topSpeed)
        {
            _rb.AddForce(new Vector3(horizontalVelocity.x, 0, horizontalVelocity.z), ForceMode.VelocityChange);
        }
        _rb.AddForce(new Vector3(_rb.velocity.x, 0, _rb.velocity.z) * -_inertia, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Sets the acceleration, topspeed, and inertia depending on wether the player is sprinting
    /// </summary>
    private void Sprint()
    {
        if (_pickupManager.CanSprint() && _sprint == 1)
        {
            _acceleration = _sprintAcceleration;
            _topSpeed = _sprintTopSpeed;
        }
        else
        {
            _acceleration = _baseAcceleration;
            _topSpeed = _baseTopSpeed;
        }

    }
}
