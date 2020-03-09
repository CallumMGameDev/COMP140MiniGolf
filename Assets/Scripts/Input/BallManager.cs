using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private bool ControllerEnabled = false;

    private void Start()
    {
        if (ControllerEnabled)
        {
            this.GetComponent<keyboardControlls>().enabled = false;
        }
        else if (!ControllerEnabled)
        {
            this.GetComponent<ArduinoControls>().enabled = false;
        }
    }
}
