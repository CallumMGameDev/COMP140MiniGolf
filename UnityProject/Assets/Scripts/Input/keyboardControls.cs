using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardControls : BallController
{
    /// <summary>
    /// Checks for when the left mouse button is held and calls the ForceApplied() function
    /// When the left mouse button is raised the force will be applied to the ball
    /// </summary>
    protected override void StationaryBall()
    {
        playerCam = Camera.main.transform.forward;
        direction = new Vector3(playerCam.x, 0, playerCam.z);
        addStroke = true;
        if (Input.GetButton("Fire1"))
        {
            ForceApplied();
            forceSlider.value = force;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            rb.AddRelativeForce(direction * force);
            force = minForce;
            state = ballState.Hit;
        }
    }
    /// <summary>
    /// When called it will add an amount of force over time and reset the foce once it reaches the maximum force
    /// </summary>
    protected override void ForceApplied()
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
