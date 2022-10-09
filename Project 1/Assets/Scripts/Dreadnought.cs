using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dreadnought : MonoBehaviour
{
    public void Shoot(bool altFire)
    {
        if (altFire == true)
        {
            Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), transform.rotation);
        }
    }

    [SerializeField]
    float speed = 0.15f;

    [SerializeField]
    Vector3 enemyPosition = Vector3.zero;

    [SerializeField]
    Vector3 direction = Vector3.left;

    [SerializeField]
    Vector3 velocity = Vector3.left;

    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    public int frameCount;

    bool altFire;

    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = transform.position;
        frameCount = 25; //NOTE: Build runs much slower than Unity gametest. When on Unity, frameCount is better at 1000. For the Build, set it to 25.
        //This is until I can figure out a better timing method
        altFire = false;
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

        //Shoot
        frameCount--;
        if (frameCount == 0)
        {
            Shoot(altFire);
            frameCount = 25;
            altFire = !altFire;
        }
    }
}
