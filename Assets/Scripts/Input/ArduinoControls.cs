using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;


public class ArduinoControls : MonoBehaviour
{
    private SerialPort serialPort;

    private void Start()
    {
        serialPort = new SerialPort("COM3", 115200);
        serialPort.Open();
        if (serialPort.IsOpen)
        {
            Debug.Log("Open");
        }
        serialPort.ReadTimeout = 20;
    }
    private void Update()
    {
        Debug.Log("Read " + serialPort.ReadLine());
    }
}