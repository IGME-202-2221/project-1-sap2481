using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dreadnought : MonoBehaviour
{
    
    public void ShipDestroyed()
    {
        SendMessageUpwards("FighterDown");
    }
    
    public void Shoot(bool altFire)
    {
        if (altFire == true)
        {
            Instantiate(bulletPrefab, new Vector3(transform.position.x - 2.5f, transform.position.y + 2.5f, transform.position.z), transform.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, new Vector3(transform.position.x - 2.5f, transform.position.y - 2.5f, transform.position.z), transform.rotation);
        }
    }

    public void Launch(bool altLaunch)
    {
        if (altLaunch == true)
        {
            Instantiate(enemyPrefab, new Vector3(transform.position.x - 2.5f, transform.position.y + 3, transform.position.z), transform.rotation, transform);
        }
        else
        {
            Instantiate(enemyPrefab, new Vector3(transform.position.x - 2.5f, transform.position.y - 3, transform.position.z), transform.rotation, transform);
        }
    }

    public void Laser()
    {
        Instantiate(laserPrefab, transform.position, transform.rotation);
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
    GameObject enemyPrefab;

    [SerializeField]
    GameObject laserPrefab;

    public int frameCount;
    public int launchLoop;

    public int laserCountdown;

    bool altFire;
    bool altLaunch;

    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = transform.position;
        frameCount = 25; //NOTE: Build runs much slower than Unity gametest. When on Unity, frameCount is better at 1000. For the Build, set it to 25.
        launchLoop = 0;
        altFire = false;

        laserCountdown = 250; //Set this to 10000 for Unity & 250 for the Build (10x more than the frameCount value)
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

        if (Time.timeScale != 0)
        {
            //Shoot
            frameCount--;
            if (frameCount == 0)
            {
                Shoot(altFire);
                frameCount = 25; //Once again, set this to 1000 on Unity and 25 for the build
                altFire = !altFire;
                launchLoop++;
            }

            //Launch
            if (launchLoop == 8)
            {
                Launch(altLaunch);
                launchLoop = 0;
                altLaunch = !altLaunch;
            }

            //Laser
            laserCountdown--;
            if (laserCountdown == 0)
            {
                Laser();
                laserCountdown = 250; //10000 for Unity, 250 for Build
            }
        }
    }
}
