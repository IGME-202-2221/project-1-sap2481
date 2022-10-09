using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    float speed = 15f;

    [SerializeField]
    Vector3 bulletPosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.right;

    [SerializeField]
    Vector3 velocity = Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        bulletPosition = transform.position;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure direction is normalized
        direction.Normalize();

        //Calculate velocity
        velocity = direction * speed * Time.deltaTime;

        //Add velocity to position
        bulletPosition += velocity;

        //Draw vehicle at that position
        transform.position = bulletPosition;
    }

}
