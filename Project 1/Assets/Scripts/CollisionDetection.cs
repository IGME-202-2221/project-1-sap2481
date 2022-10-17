using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    bool Collision(GameObject vehicle, GameObject obstacle)
    {       
        bool isColliding;

        if (vehicle.GetComponent<SpriteRenderer>().bounds.min.x < obstacle.GetComponent<SpriteRenderer>().bounds.max.x &&
            vehicle.GetComponent<SpriteRenderer>().bounds.max.x > obstacle.GetComponent<SpriteRenderer>().bounds.min.x &&
            vehicle.GetComponent<SpriteRenderer>().bounds.min.y < obstacle.GetComponent<SpriteRenderer>().bounds.max.y &&
            vehicle.GetComponent<SpriteRenderer>().bounds.max.y > obstacle.GetComponent<SpriteRenderer>().bounds.min.y)
        {
            isColliding = true;

            if (obstacle.tag == "EnemyBullet" || obstacle.tag == "PlayerBullet" || obstacle.tag == "EnemyShip")
            {
                Destroy(obstacle);
            }
        }
        else
        {
            isColliding = false;
        }

        return isColliding;
    }

    public void FighterDown()
    {
        BroadcastMessage("ScoreUp", 100f);
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
                BroadcastMessage("PlayerDamage", 3.0f);
                break;
            }
        }
        
        //Check for Player Bullet Collisions
        for (int i = 0; i < playerBulletList.Count; i++)
        {
            if (Collision(dreadnought, playerBulletList[i]))
            {
                BroadcastMessage("DreadnoughtDamage", 5f);
                break;
            }
        }

        //Check for Enemy Ship Collisions - Player vs Enemy
        for (int i = 0; i < enemyShipList.Count; i++)
        {
            if (Collision(player, enemyShipList[i]))
            {
                BroadcastMessage("PlayerDamage", 10f);
                Destroy(enemyShipList[i]);
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
                    BroadcastMessage("EnemyDamage", 1f);
                    break;
                }
            }
        }

        //Check for Laser Collisions
        for (int i = 0; i < laserList.Count; i++)
        {
            if (Collision(player, laserList[0]))
            {
                BroadcastMessage("PlayerDamage", 1f);
            }
        }

        //Check for Ship-To-Ship Collision
        if (Collision(player, dreadnought))
        {
            BroadcastMessage("PlayerDamage", 2f);
        }

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
