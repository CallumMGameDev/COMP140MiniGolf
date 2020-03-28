using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardControls : BallController
{
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
