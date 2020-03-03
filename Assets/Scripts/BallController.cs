using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private enum ballState
    {
        Stationary,
        Hit,
    }

    

    private Rigidbody rb;
    private Vector3 playerCam;
    [SerializeField]
    private Vector3 direction;
    [Header("Force On The Ball")]
    [SerializeField]
    private float force;
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float minForce;
    [SerializeField]
    private float addForce;
    [SerializeField]
    private Slider forceSlider;
    [SerializeField]
    private ballState state;


    private void Start()
    {
        forceSlider.maxValue = maxForce;
        forceSlider.minValue = minForce;
        force = minForce;
        rb = this.GetComponent<Rigidbody>();
        state = ballState.Stationary;
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case ballState.Stationary:
                StationaryBall();
                break;
            case ballState.Hit:
                BallHit();
                break;
        }
    }


    private void StationaryBall()
    {
        playerCam = Camera.main.transform.forward;
        direction = new Vector3(playerCam.x, 0, playerCam.z);
        if (Input.GetButton("Fire1"))
        {
            IncreaseForce();
            forceSlider.value = force;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            rb.AddRelativeForce(direction * force);
            force = minForce;
            state = ballState.Hit;
        }
    }

    private void IncreaseForce()
    {
        if (force <= maxForce)
        {
            force += Time.deltaTime * addForce;
        }
        else if (force > maxForce)
        {
            force = minForce;
        }
    }

    private void BallHit()
    {
        if(rb.velocity == Vector3.zero)
        {
            state = ballState.Stationary;
        }
    }
}
