using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform fpCamera;
    public float verticalCameraSensitivity = 10;
    public float verticalCameraLimit = 10;
    float xRotation;
    float oldXRotation = 0;

    // Start is called before the first frame update
    private void Start()
    {
        transform.rotation.eulerAngles.Set(0,0,0);
    }

    // Update is called once per frame
    private void Update()
    {
        xRotation += Input.GetAxis("Mouse Y");        
        xRotation = Mathf.Clamp(xRotation, -verticalCameraLimit, verticalCameraLimit);
        float xRotationDifference = xRotation - oldXRotation;
        oldXRotation = xRotation;
        transform.Rotate(xRotationDifference * -1 * verticalCameraSensitivity, 0, 0);
    }
}
