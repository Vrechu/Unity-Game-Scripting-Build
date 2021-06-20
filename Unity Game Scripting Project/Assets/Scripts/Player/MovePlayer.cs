using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 10;
    [SerializeField] private float _horizontalCameraSensitivity = 10;
    private float _movementSpeed;

    private float _forward;
    private float _sideways;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movementSpeed = _baseSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MoveHorizontally();
    }

    private void Update()
    {
        SetKeys();
        transform.Rotate(0, Input.GetAxis("Mouse X") * _horizontalCameraSensitivity, 0);        
    }

    /// <summary>
    /// sets the horizontal imput keys
    /// </summary>
    private void SetKeys()
    {
        _forward = Input.GetAxis("Vertical");
        _sideways = Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// sets the player velocity
    /// </summary>
    private void MoveHorizontally()
    {
        Vector3 zVelocity = transform.forward * _forward;
        Vector3 xVelocity = transform.right * _sideways;
        Vector3 horizontalVelocity = (zVelocity + xVelocity).normalized * _movementSpeed;
        _rb.velocity = new Vector3(horizontalVelocity.x, _rb.velocity.y, horizontalVelocity.z);
    }
}
