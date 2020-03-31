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

    private string serialRead;
    private string[] neededValues;
    private int length;

    /// <summary>
    /// Sets up the serialport so that unity can read the values from the serialport
    /// As well as doing the same set up as the BallController script 
    /// This is because I am overiding the start function in order to add the arduino code 
    /// </summary>
    protected override void Start()
    {
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

    /// <summary>
    /// checks if the acceleration is higher than a certain value before applying a force to the ball 
    /// to prevent the ball being launched when the user has had no input
    /// </summary>
    protected override void StationaryBall()
    {
        playerCam = Camera.main.transform.forward;

        if (xAccel >= 700 || yAccel >= 700)
        { 
            rb.AddRelativeForce(arduinoForce);
            state = ballState.Hit;
        }
    }

    /// <summary>
    /// Checks if the ball is stationary
    /// as well as checking if the acceleration is less than a specific value
    /// to help reduce the ball being hit when the user doesn't have any input
    /// </summary>
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

    /// <summary>
    /// Reads the values from the serial port 
    /// This will then split each value up and convert them to a integer
    /// Where they are then applied to the correct variable
    /// </summary>
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
                xGyro = result;
            }
            if (i == 4)
            {
                yGyro = result;
            }
            if (i == 5)
            {
                zGyro = result;
            }
        }
        SetForces();
        //direction = new Vector3(xGyro, 0, zGyro);
        Debug.Log(xAccel + " " + yAccel + " " + zAccel + " " + xGyro + " " + yGyro + " " + zGyro);
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        getArduinoValues();
    }

    /// <summary>
    /// This will simply set the force to the value depending on the value read from the arduino
    /// The values are being divided by for so that it is easier to get the lower values 
    /// Otherwise in testing it would always be the max force applied
    /// </summary>
    private void SetForces()
    {
        if(xAccel >= maxForce * 4)
        {
            if(xAccel < 0)
            {
                xForce = 0;
            }
            xForce = maxForce;
        }
        else
        {
            xForce = xAccel / 4;
        }

        if(yAccel >= maxForce * 4)
        {
            if(yAccel < 0)
            {
                zForce = 0;
            }
            zForce = maxForce;
        }
        else
        {
            zForce = yAccel / 4;
        }
        arduinoForce = new Vector3(xForce, 0, zForce);
    }
}