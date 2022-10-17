using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
    
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

    [SerializeField]
    public GameObject bulletPrefab;

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
        if (vehiclePosition.x > (width / 2))
        {
            vehiclePosition.x = width / 2;
        }
        if (vehiclePosition.x < -(width / 2))
        {
            vehiclePosition.x = -(width / 2);
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

        if (Time.timeScale != 0)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Shoot();
            }
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}
