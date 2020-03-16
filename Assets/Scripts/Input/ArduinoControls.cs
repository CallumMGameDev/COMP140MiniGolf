using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;


public class ArduinoControls : BallController
{
    private SerialPort serialPort;
    [SerializeField]
    private int xAccelOffset;
    [SerializeField]
    private int yAccelOffset;
    [SerializeField]
    private int zAccelOffset;
    [SerializeField]
    private int xGyroOffset;
    [SerializeField]
    private int yGyroOffset;
    [SerializeField]
    private int zGyroOffset;

    private int xAccel;
    private int yAccel;
    private int zAccel;
    private int xGyro;
    private int yGyro;
    private int zGyro;

    private string serialRead;
    private string[] neededValues;
    private int length;
    private int[] asInt;
    protected override void Start()
    {
        serialPort = new SerialPort("COM3", 115200);
        serialPort.Open();
        if (serialPort.IsOpen)
        {
            Debug.Log("Open");
        }

        serialPort.ReadTimeout = 20;

        forceSlider.maxValue = maxForce;
        forceSlider.minValue = minForce;
        force = minForce;
        rb = this.GetComponent<Rigidbody>();
        state = ballState.Stationary;
        stokeCount.SetText("Strokes " + strokes);
    }
    protected override void StationaryBall()
    {
        playerCam = Camera.main.transform.forward;
        direction = new Vector3(playerCam.x, 0, playerCam.z);

        serialRead = serialPort.ReadLine();
        neededValues = serialRead.Split();
        length = neededValues.Length;
        int result;
        for (int i = 0; i < length; i++)
        {
            int.TryParse(neededValues[i], out result);
            if (i == 0)
            {
                xAccel = result + xAccelOffset;
            }
            if (i == 1)
            {
                yAccel = result + yAccelOffset;
            }
            if (i == 2)
            {
                zAccel = result + zAccelOffset;
            }
            if (i == 3)
            {
                xGyro = result + xGyroOffset;
            }
            if (i == 4)
            {
                yGyro = result + yGyroOffset;
            }
            if (i == 5)
            {
                zGyro = result + zGyroOffset;
            }
        }
        
        if (xAccel >= 500)
        {
            force = xAccel;
            rb.AddRelativeForce(direction * force);
            state = ballState.Hit;
        }
    }
}