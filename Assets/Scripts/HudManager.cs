using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Text scoreLabel;
    public Text livesLabel;
    public Text levelLabel;
 
    private static HudManager hudInstance;

    void Awake(){ 
        if (hudInstance == null) {
            DontDestroyOnLoad (this);
            hudInstance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        Refresh();
    }
 
    // Show player stats in the HUD
    public void Refresh()
    {
        scoreLabel.text = "Score: " + GameManager.instance.score;
        livesLabel.text = "Lives: " + GameManager.instance.lives;
        levelLabel.text = "Level " + GameManager.instance.current_level;
    }
}
