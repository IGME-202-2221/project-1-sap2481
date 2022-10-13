using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    float speed = -20f;

    [SerializeField]
    Vector3 laserPosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.left;

    [SerializeField]
    Vector3 velocity = Vector3.left;

    float velocityY;
    float velocityX;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        laserPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        transform.eulerAngles = new Vector3(0, 0, 0);

        //Calculate Velocity at Start
        velocityX = speed * Time.deltaTime;
        velocityY = (target.transform.position.y - transform.position.y) * Time.deltaTime;

        //Rotate Bullet
        var offset = 90f;
        Vector2 newDirection = target.transform.position - transform.position;
        newDirection.Normalize();
        float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure direction is normalized
        direction.Normalize();

        //Add velocity to position
        laserPosition.x += velocityX;
        laserPosition.y += velocityY;

        //Draw vehicle at that position
        transform.position = laserPosition;
    }
}
