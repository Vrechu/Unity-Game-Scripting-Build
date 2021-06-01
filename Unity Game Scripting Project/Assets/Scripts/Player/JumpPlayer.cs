using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    public float jumpForce = 300;

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

    private void Jump()
    {
        if (IsGrounded()
             && _jump == 1)
        {
            _rb.AddForce(transform.up * jumpForce);
        }
    }
    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, 1.1f))
        {
            return true;
        }
        else return false;
    }
}
