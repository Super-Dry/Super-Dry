using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class pauseGame : MonoBehaviour
{
    Canvas myCanvas;
    public static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame(){
        Time.timeScale = 0f; // Pause the game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Get the Canvas component of the GameObject this script is attached to
        myCanvas = GetComponent<Canvas>();
        // Check if the Canvas component was found
        if (myCanvas == null)
        {
            Debug.LogError("No Canvas component found on this GameObject!");
        }
        else{
            myCanvas.enabled = true;
        }
        isPaused = true;
    
    }
    public void ResumeGame()
    {

        Time.timeScale = 1f; // Unpause the game
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor back for the game
        Cursor.visible = true; // hide the cursor
        //Canvas canvas = canvasObject.GetComponent<Canvas>(); // get the canvas component
        myCanvas = GetComponent<Canvas>();
        // Check if the Canvas component was found
        if (myCanvas == null)
        {
            Debug.LogError("No Canvas component found on this GameObject!");
        }
        else{
            myCanvas.enabled = false;
        }
        isPaused = false;
    }
}
