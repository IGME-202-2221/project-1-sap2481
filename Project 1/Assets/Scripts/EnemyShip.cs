using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    
    [SerializeField]
    float speed = 7f;

    [SerializeField]
    Vector3 shipPosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.left;

    [SerializeField]
    Vector3 velocity = Vector3.left;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        shipPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        transform.eulerAngles = new Vector3(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure direction is normalized
        direction.Normalize();

        //Calculate velocity
        velocity = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        //Add velocity to position
        shipPosition = velocity;

        //Draw vehicle at that position
        transform.position = shipPosition;

        //Rotate towards player
        var offset = -90f;
        Vector2 newDirection = target.transform.position - transform.position;
        newDirection.Normalize();
        float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
