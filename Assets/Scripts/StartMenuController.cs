using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour {

    // Run on the first frame
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitGame()
    {
        // Close the game
        Application.Quit();
    }

    public void StartGame(){
        // Load first scene
        SceneManager.LoadScene("Level 1");
    }
}
