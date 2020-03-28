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

    private float xForce;
    private float zForce;
    [SerializeField]
    private Vector3 arduinoForce;
    private Vector3 maxArduinoForce;

    private string serialRead;
    private string[] neededValues;
    private int length;
    protected override void Start()
    {
        maxArduinoForce = new Vector3(maxForce, 0, 0);
        serialPort = new SerialPort("COM3", 38400);
        serialPort.Open();
        if (serialPort.IsOpen)
        {
            Debug.Log("Open");
        }

        serialPort.ReadTimeout = 500;

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

        if (xAccel >= 700 || yAccel >= 700)
        { 
            rb.AddRelativeForce(arduinoForce);
            state = ballState.Hit;
        }
    }

    protected override void BallHit()
    {
        
        if (addStroke == true)
        {
            strokes++;
            stokeCount.SetText("Strokes " + strokes);
            addStroke = false;
        }
        if (rb.velocity == Vector3.zero )
        {
            if (xAccel <= -800)
            {
                state = ballState.Stationary;
            }
        }
    }

    private void getArduinoValues()
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
                if (result + xGyroOffset >= -25 || result + xGyroOffset <= 25)
                {
                    xGyro = 1;
                }
                else
                {
                    xGyro = result;
                }
            }
            if (i == 4)
            {
                yGyro = result + yGyroOffset;
            }
            if (i == 5)
            {
                if (result + zGyroOffset >= -25 || result + zGyroOffset <= 25)
                {
                    zGyro = 0;
                }
                else
                {
                    zGyro = result;
                }
            }
        }
        SetForces();
        //direction = new Vector3(xGyro, 0, zGyro);
        Debug.Log(xAccel + " " + yAccel + " " + zAccel + " " + xGyro + " " + yGyro + " " + zGyro);
    }

    private void FixedUpdate()
    {
        getArduinoValues();
    }

    private void SetForces()
    {
        if(xAccel >= maxForce * 4)
        {
            xForce = maxForce;
        }
        else
        {
            xForce = xAccel / 4;
        }

        if(yAccel >= maxForce * 4)
        {
            zForce = maxForce;
        }
        else
        {
            zForce = yAccel / 4;
        }
        arduinoForce = new Vector3(xForce, 0, zForce);
    }
}