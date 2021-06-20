using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _verticalCameraSensitivity = 10;
    [SerializeField] private float _verticalCameraLimit = 10;
    private float _xRotation;
    private float _oldXRotation = 0;

    private void Start()
    {
        transform.rotation.eulerAngles.Set(0,0,0);
    }

    private void Update()
    {
        _xRotation += Input.GetAxis("Mouse Y");        
        _xRotation = Mathf.Clamp(_xRotation, -_verticalCameraLimit, _verticalCameraLimit);
        float xRotationDifference = _xRotation - _oldXRotation;
        _oldXRotation = _xRotation;
        transform.Rotate(xRotationDifference * -1 * _verticalCameraSensitivity, 0, 0);
    }
}
