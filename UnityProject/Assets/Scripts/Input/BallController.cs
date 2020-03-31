using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BallController : MonoBehaviour
{
    protected enum ballState
    {
        Stationary,
        Hit,
    }


    protected Rigidbody rb;
    protected Vector3 playerCam;
    [SerializeField]
    protected Vector3 direction;
    [Header("Force On The Ball")]
    [SerializeField]
    protected float force;
    [SerializeField]
    protected float maxForce;
    [SerializeField]
    protected float minForce;
    [SerializeField]
    protected float addForce;
    [SerializeField]
    protected Slider forceSlider;
    [SerializeField]
    protected TextMeshProUGUI stokeCount;

    [SerializeField]
    protected ballState state;
    public int strokes;
    protected bool addStroke = true;

    /// <summary>
    /// Sets all the variables up that are nessasary for the ball to have basic functionality 
    /// </summary>
    protected virtual void Start()
    {
        forceSlider.maxValue = maxForce;
        forceSlider.minValue = minForce;
        force = minForce;
        rb = this.GetComponent<Rigidbody>();
        state = ballState.Stationary;
        stokeCount.SetText("Strokes " + strokes);
    }
    /// <summary>
    /// A basic way to switch between the state of the ball so that the ball can only be hit when its stationary
    /// </summary>
    protected  virtual void Update()
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


    protected virtual void StationaryBall()
    {

    }

    protected virtual void ForceApplied()
    {

    }

    /// <summary>
    /// Checks if the ball has been hit and prevents the user from hitting the ball whilst its still moving
    /// </summary>
    protected virtual void BallHit()
    {
        if (addStroke == true)
        {
            strokes++;
            stokeCount.SetText("Strokes " + strokes);
            addStroke = false;
        }
        if (rb.velocity == Vector3.zero)
        {
            state = ballState.Stationary;
        }
    }
}
