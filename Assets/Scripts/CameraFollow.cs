using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lookAt;

    private Vector3 currentMouse;
    private Vector3 cursorDirection;
    public float sensitivity = 3f;
    public float distance = 5f;

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                currentMouse.x += Input.GetAxis("Mouse X") * sensitivity;
                currentMouse.y -= Input.GetAxis("Mouse Y") * sensitivity;

                cursorDirection = lookAt.position - transform.position;

                currentMouse.y = Mathf.Clamp(currentMouse.y, 0f, 90f);

                Quaternion rotation = Quaternion.Euler(currentMouse.y, currentMouse.x, 0);

                transform.position = lookAt.position - rotation * (Vector3.forward * distance);
                transform.rotation = rotation;
            }
        }
    }
}