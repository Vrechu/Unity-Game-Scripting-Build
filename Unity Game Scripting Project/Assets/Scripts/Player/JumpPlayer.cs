using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 300;

    private Rigidbody _rb;
    private float _jump;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SetKey();
        Jump();
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
            _rb.AddForce(transform.up * _jumpForce);
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
