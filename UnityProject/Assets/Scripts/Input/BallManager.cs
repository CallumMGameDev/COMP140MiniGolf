using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private bool controllerEnabled = false;

    private void Start()
    {
        if (controllerEnabled)
        {
            this.GetComponent<keyboardControlls>().enabled = false;
        }
        else if (!controllerEnabled)
        {
            this.GetComponent<ArduinoControls>().enabled = false;
        }
    }
}
