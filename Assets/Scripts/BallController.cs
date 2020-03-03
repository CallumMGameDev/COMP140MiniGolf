using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
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


    private void Start()
    {
        forceSlider.maxValue = maxForce;
        forceSlider.minValue = minForce;
        force = minForce;
        rb = this.GetComponent<Rigidbody>();
    }
    private void Update()
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
}
