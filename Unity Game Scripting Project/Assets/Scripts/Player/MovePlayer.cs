using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float baseSpeed = 10;
    public float horizontalCameraSensitivity = 10;
    private float _movementSpeed;

    private float _forward;
    private float _sideways;

    private Rigidbody _rb;


    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movementSpeed = baseSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SetKeys();
        MoveHorizontally();
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * horizontalCameraSensitivity, 0);        
    }

    private void SetKeys()
    {
        _forward = Input.GetAxis("Vertical");
        _sideways = Input.GetAxis("Horizontal");
    }

    private void MoveHorizontally()
    {
        Vector3 zVelocity = transform.forward * _forward;
        Vector3 xVelocity = transform.right * _sideways;
        Vector3 horizontalVelocity = (zVelocity + xVelocity).normalized * _movementSpeed;
        _rb.velocity = new Vector3(horizontalVelocity.x, _rb.velocity.y, horizontalVelocity.z);
    }
}
