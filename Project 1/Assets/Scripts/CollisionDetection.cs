using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    bool Collision(GameObject vehicle, GameObject obstacle)
    {
        bool isColliding;
        if (vehicle.GetComponent<BoxCollider2D>().bounds.min.x < obstacle.GetComponent<BoxCollider2D>().bounds.max.x &&
            vehicle.GetComponent<BoxCollider2D>().bounds.max.x > obstacle.GetComponent<BoxCollider2D>().bounds.min.x &&
            vehicle.GetComponent<BoxCollider2D>().bounds.min.y < obstacle.GetComponent<BoxCollider2D>().bounds.max.y &&
            vehicle.GetComponent<BoxCollider2D>().bounds.max.y > obstacle.GetComponent<BoxCollider2D>().bounds.min.y)
        {
            isColliding = true;
            vehicle.GetComponent<SpriteRenderer>().color = Color.red;
            //NOTE: I know why the red coloration on collision for the dreadnought is broken. I just haven't been able to figure out how to fix it yet. Stay tuned.
            if (vehicle.tag == "EnemyShip")
            {
                Destroy(vehicle);
            }
            if (obstacle.tag == "EnemyBullet" || obstacle.tag == "PlayerBullet" || obstacle.tag == "EnemyShip")
            {
                Destroy(obstacle);
            }
            else
            {
                obstacle.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            isColliding = false;
            vehicle.GetComponent<SpriteRenderer>().color = Color.white;
        }
        return isColliding;
    }

    GameObject player;
    GameObject dreadnought;
    GameObject enemy;
    List<GameObject> enemyBulletList;
    List<GameObject> playerBulletList;
    List<GameObject> enemyShipList;
    List<GameObject> laserList;

    [SerializeField]
    Camera cam;

    [SerializeField, HideInInspector]
    float height;

    [SerializeField, HideInInspector]
    float width;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Ship");
        dreadnought = GameObject.Find("Dreadnought");
        playerBulletList = new List<GameObject>();
        enemyBulletList = new List<GameObject>();
        enemyShipList = new List<GameObject>();
        laserList = new List<GameObject>();

        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Bullets
        playerBulletList.Clear();
        GameObject[] playerBulletFind = GameObject.FindGameObjectsWithTag("PlayerBullet");
        for (int i = 0; i < playerBulletFind.Length; i++)
        {
            playerBulletList.Add(playerBulletFind[i]);
        }

        //Enemy Bullets
        enemyBulletList.Clear();
        GameObject[] enemyBulletFind = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < enemyBulletFind.Length; i++)
        {
            enemyBulletList.Add(enemyBulletFind[i]);
        }

        //Enemy Ships
        enemyShipList.Clear();
        GameObject[] enemyShipFind = GameObject.FindGameObjectsWithTag("EnemyShip");
        for (int i = 0; i < enemyShipFind.Length; i++)
        {
            enemyShipList.Add(enemyShipFind[i]);
        }

        //Lasers
        laserList.Clear();
        GameObject[] laserFind = GameObject.FindGameObjectsWithTag("Laser");
        for (int i = 0; i < laserFind.Length; i++)
        {
            laserList.Add(laserFind[i]);
        }

        //Check for Enemy Bullet Collisions
        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (Collision(player, enemyBulletList[i]))
            {
                break;
            }
        }

        //Check for Player Bullet Collisions
        for (int i = 0; i < playerBulletList.Count; i++)
        {
            if (Collision(dreadnought, playerBulletList[i]))
            {
                break;
            }
        }

        //Check for Enemy Ship Collisions - Player vs Enemy
        for (int i = 0; i < enemyShipList.Count; i++)
        {
            if (Collision(player, enemyShipList[i]))
            {
                break;
            }
        }

        //Check for Enemy Ship Collisions - Enemy vs Player Bullet
        for (int i = 0; i < enemyShipList.Count; i++)
        {
            for (int j = 0; j < playerBulletList.Count; j++)
            {
                if (Collision(enemyShipList[i], playerBulletList[j]))
                {
                    break;
                }
            }
        }

        //Check for Ship-To-Ship Collision
        Collision(player, dreadnought);

        //Remove Enemy Bullets If Outside the Screen
        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (enemyBulletList[i].transform.position.y > height / 2 || enemyBulletList[i].transform.position.y < -(height / 2)
                || enemyBulletList[i].transform.position.x < -(width / 2))
            {
                Destroy(enemyBulletList[i]);
            }
        }

        //Remove Lasers If Outside the Screen
        for (int i = 0; i < laserList.Count; i++)
        {
            if (laserList[i].transform.position.y > height / 2 || laserList[i].transform.position.y < -(height / 2)
                || laserList[i].transform.position.x < -(width / 2))
            {
                Destroy(laserList[i]);
            }
        }
    }
}
