using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public void PlayerDamage(float dmg)
    {
        playerHealth -= dmg;
    }
    public void DreadnoughtDamage(float dmg)
    {
        enemyHealth -= dmg;
        score += dmg;
    }
    public void ScoreUp(float sc)
    {
        score += sc;
    }
    
    public float score = 0;
    public float playerHealth = 100;
    public float enemyHealth = 1000;

    bool textOn;

    [SerializeField]
    Text scoreLabel;

    [SerializeField]
    Slider healthBarPlayer;

    [SerializeField]
    Slider healthBarEnemy;

    [SerializeField]
    GameObject gameOverPrefab;

    [SerializeField]
    GameObject youWinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        textOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "SCORE: " + score;
        healthBarPlayer.value = playerHealth;
        healthBarEnemy.value = enemyHealth;

        if (textOn == false)
        {
            if (playerHealth <= 0)
            {
                Instantiate(gameOverPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Time.timeScale = 0;
                textOn = true;
            }

            if (enemyHealth <= 0)
            {
                Instantiate(youWinPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Time.timeScale = 0;
                textOn = true;
                Destroy(GameObject.Find("Dreadnought"));
            }
        }
    }
}
