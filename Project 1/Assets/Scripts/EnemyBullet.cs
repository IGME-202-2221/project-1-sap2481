using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float speed = -15f;

    [SerializeField]
    Vector3 bulletPosition = Vector3.zero;

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
        bulletPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        transform.eulerAngles = new Vector3(0, 0, 0);

        //Calculate Velocity at Start
        velocityX = speed * Time.deltaTime;
        velocityY = (target.transform.position.y - transform.position.y) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {    
        //Make sure direction is normalized
        direction.Normalize();

        //Add velocity to position
        bulletPosition.x += velocityX;
        bulletPosition.y += velocityY;

        //Draw vehicle at that position
        transform.position = bulletPosition;
    }
}
