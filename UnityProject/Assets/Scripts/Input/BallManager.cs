using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject ballCamera;
    [SerializeField]
    private bool controllerEnabled = false;
    /// <summary>
    /// Uses a simple bool that the player can enable to determain if they are using the custom controller
    /// and therefore it will disable the script which isn't required
    /// </summary>
    private void Start()
    {
        if (controllerEnabled)
        {
            ball.GetComponent<keyboardControls>().enabled = false;
        }
        else if (!controllerEnabled)
        {
            ball.GetComponent<ArduinoControls>().enabled = false;
        }
    }

    private void Update()
    {
        if(ball == null)
        {
            ballCamera.GetComponent<CameraFollow>().enabled = false;
        }
    }
}
