using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10;

    private Rigidbody _rb;
    private float _jump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {        
        Jump();
    }

    private void Update()
    {
        SetKey();
    }

    private void SetKey()
    {
        _jump = Input.GetAxis("Jump");
    }

    /// <summary>
    /// Adds an upward force to the player when the key is pressed and the player is grounded
    /// </summary>
    private void Jump()
    {
        if (IsGrounded()
             && _jump == 1)
        {
            _rb.AddForce(transform.up * _jumpForce,ForceMode.Impulse);
        }
    }

    /// <summary>
    /// checks if the player is grounded
    /// </summary>
    /// <returns>returns true if player is grounded</returns>
    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, 1.01f))
        {
            return true;
        }
        else return false;
    }
}
