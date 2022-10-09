using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public float score = 0;
    public float playerHealth = 100;

    [SerializeField]
    Text scoreLabel;

    [SerializeField]
    Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "SCORE: " + score;
        healthBar.value = playerHealth;
    }
}
