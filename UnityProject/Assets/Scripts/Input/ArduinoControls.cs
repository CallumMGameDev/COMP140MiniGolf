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
        //direction = new Vector3(playerCam.x, 0, playerCam.z);

        getArduinoValues();
        if (xAccel >= 500)
        {
            if (xAccel >= maxForce * 4)
            {
                force = maxForce;
            }
            else
            {
                force = xAccel / 4;
            }
                
            rb.AddRelativeForce(direction * force);
            state = ballState.Hit;
        }
    }

    protected override void BallHit()
    {
        getArduinoValues();
        if (addStroke == true)
        {
            strokes++;
            stokeCount.SetText("Strokes " + strokes);
            addStroke = false;
        }
        if (rb.velocity == Vector3.zero )
        {
            if (xAccel <= -500)
            {
                state = ballState.Stationary;
            }
        }
    }

    protected void getArduinoValues()
    {
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
                if (result + xGyroOffset >= -50 || result + xGyroOffset <= 50)
                {
                    xGyro = 1;
                } 
            }
            if (i == 4)
            {
                yGyro = result + yGyroOffset;
            }
            if (i == 5)
            {
                if (result + zGyroOffset >= -50 || result + zGyroOffset <= 50)
                {
                    zGyro = 0;
                }
            }
        }
        direction = new Vector3(xGyro, 0, zGyro);
        Debug.Log(xAccel + " " + yAccel + " " + zAccel + " " + xGyro + " " + yGyro + " " + zGyro);
    }
}