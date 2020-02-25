using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private float rotationSpeed;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = targetObj.transform;
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {

        //Zoom in and Zoom Out
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target.position, target.up, Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(target.position, -target.up, Time.deltaTime * rotationSpeed);

        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(target.position, target.forward, Time.deltaTime * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(target.position, -target.forward, Time.deltaTime * rotationSpeed);
        }
    }
}
