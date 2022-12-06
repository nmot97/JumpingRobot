using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompletedController : MonoBehaviour {

    // Where the score value will be shown
    public Text score;

    // Run on the first frame
    void Start()
    {
        // Show the score and high score
        score.text = GameManager.instance.score.ToString();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitGame()
    {
        // Close the game
        Application.Quit();
    }
}