using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    private Vector3 currentMouse;
    private Vector3 cursorDirection;
    public float sensitivity = 3f;
    public float distance = 5f;
    public Quaternion rotation;

    /// <summary>
    /// simply checks if the right mouse button is held down
    /// and allows the user to move the camera when using keyboard controls
    /// </summary>
    private void Update()
    {
        if(ball != null)
        {
            if (Input.GetButton("Fire2"))
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    currentMouse.x += Input.GetAxis("Mouse X") * sensitivity;
                    currentMouse.y -= Input.GetAxis("Mouse Y") * sensitivity;

                    cursorDirection = ball.transform.position - transform.position;

                    currentMouse.y = Mathf.Clamp(currentMouse.y, 0f, 90f);

                    rotation = Quaternion.Euler(currentMouse.y, currentMouse.x, 0);
                }
            }
            transform.position = ball.transform.position - rotation * (Vector3.forward * distance);
            transform.rotation = rotation;
        }
    }
}