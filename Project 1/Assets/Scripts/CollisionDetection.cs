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
            if (obstacle.tag == "EnemyBullet" || obstacle.tag == "PlayerBullet")
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
    }
}
