using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dreadnought : MonoBehaviour
{
    [SerializeField]
    float speed = 0.15f;

    [SerializeField]
    Vector3 enemyPosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.left;

    [SerializeField]
    Vector3 velocity = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure direction is normalized
        direction.Normalize();

        //Calculate velocity
        velocity = direction * speed * Time.deltaTime;

        //Add velocity to position
        enemyPosition += velocity;

        //Draw vehicle at that position
        transform.position = enemyPosition;
    }
}
