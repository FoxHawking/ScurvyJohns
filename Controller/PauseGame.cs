using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Pause game class, handles behaviour for the game being paused
/// </summary>
public class PauseGame : MonoBehaviour
{
    // Keeps track of whether the game is paused or not
    public static bool isPaused = false;

    // pause menu object
    public GameObject pauseMenuUI;

    //start function
    private void Start()
    {
        Unpause();
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        // Pause button = Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Function that unpauses the game
    /// </summary>
    public void Unpause()
    {

        // Disable pause menu UI
        pauseMenuUI.SetActive(false);
        // Unpause time
        Time.timeScale = 1f;
        // Update isPaused variable
        isPaused = false;

    }

    /// <summary>
    /// Function that pauses the game
    /// </summary>
    public void Pause ()
    {
        // Enable pause menu UI
        pauseMenuUI.SetActive(true);
        // Pause time
        Time.timeScale = 0f;
        // Update isPaused variable
        isPaused = true;
    }
}
