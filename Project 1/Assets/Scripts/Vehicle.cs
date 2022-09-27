using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    Vector3 vehiclePosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.zero;

    [SerializeField]
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    float turnAmount = 0;

    [SerializeField]
    Camera cam;

    [SerializeField, HideInInspector]
    float height;

    [SerializeField, HideInInspector]
    float width;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure direction is normalized
        direction.Normalize();

        //Turn the vehicle by some angle
        direction = Quaternion.EulerAngles(0, 0, turnAmount * Time.deltaTime) * direction;
        
        //Calculate velocity
        velocity = direction * speed * Time.deltaTime;

        //Add velocity to position
        vehiclePosition += velocity;

        //Draw vehicle at that position
        transform.position = vehiclePosition;

        //Don't Allow Wrap Over X
        if (vehiclePosition.x > 10)
        {
            vehiclePosition.x = 10;
        }
        if (vehiclePosition.x < -10)
        {
            vehiclePosition.x = -10;
        }
        //Allow Wrap Over Y
        if (vehiclePosition.y > (height / 2))
        {
            vehiclePosition.y = -(height / 2);
        }
        if (vehiclePosition.y < -(height / 2))
        {
            vehiclePosition.y = (height / 2);
        } 
    }

    public void OnMove(InputAction.CallbackContext context) //I don't need this element of OnMove, as I want the ship to consistently face the same direction
    {
        direction = context.ReadValue<Vector2>();

        //transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }
}
