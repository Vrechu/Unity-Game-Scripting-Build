using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform fpCamera;
    public float verticalCameraSensitivity = 10;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(transform.rotation.eulerAngles.x);
        float xRotation = transform.rotation.eulerAngles.x;
        
        if (xRotation > 180)
        {
            xRotation -= 360;
        }

        if (xRotation < 90)
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * -1 * verticalCameraSensitivity, 0, 0);
            
        }


        transform.rotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
