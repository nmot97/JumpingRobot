using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static instance of the Game Manager,
    // can be accessed from anywhere
    public static GameManager instance = null;

    // HUD
    public HudManager hud;

    // Player
    public PlayerController thePlayer;
 
    // Player score
    public int score = 0;
    
    // High score
    public int highScore = 0;

    // Lives
    public int lives = 5;

    // Respawn
    private bool isRespawning;
    private Vector3 respawnPoint;

    public int current_level = 1;
    public int highest_level = 3;
 
    // Called when the object is initialized
    void Awake()
    {
        // if it doesn't exist
        if(instance == null)
        {
            // Set the instance to the current object (this)
            instance = this;
        }
 
        // There can only be a single instance of the game manager
        else if(instance != this)
        {
            // Destroy the current object, so there is just one manager
            Destroy(gameObject);
        }
 
        // Don't destroy this object when loading scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        respawnPoint = new Vector3(0,2,0);
    }
 
    // Increase score
    public void IncreaseScore(int amount){
        // Increase the score by the given amount
        score += amount;
 
        // Update hud with new score
        hud.Refresh();

        //Update high score
        if (score > highScore)
        {
            highScore = score;
        }
    }

    // Decrease life
    public void HurtPlayer(int amount){
        // Decrease the lives by the given amount
        lives -= amount;
 
        // Update hud with new lives
        hud.Refresh();

        if(lives <= 0){
            EndGame();
        }
        else{
            Respawn();
        }
    }

    public void Respawn(){
        CharacterController cc = thePlayer.GetComponent<CharacterController>();
        cc.enabled = false;
        thePlayer.transform.position = respawnPoint;
        cc.enabled = true;
    }

    // Go to the next level (next scene)
    public void IncreaseLevel()
    {
        current_level++;

        if (current_level <= highest_level)
        {
            SceneManager.LoadScene("Level " + current_level);
            Respawn();
            hud.Refresh();
        }
        else
        {
            SceneManager.LoadScene("GameCompleted");
            Destroy(thePlayer.gameObject);
            Destroy(hud.gameObject);
        }
    }

    // Change the respawn point
    public void SetRespawnPoint(Vector3 spawn){
        respawnPoint = spawn;
    }

    public void EndGame(){
        SceneManager.LoadScene("GameOver");
    }

    // Restart game. Refresh previous score and send back to level 1
    public void Reset()
    {
        // Reset the score
        score = 0;

        // Reset lives
        lives = 5;
 
        // Set the current level to 1
        current_level = 1;
        
        // Load corresponding scene
        SceneManager.LoadScene("Level 1");

        Respawn();

        hud.Refresh();
    }
}
