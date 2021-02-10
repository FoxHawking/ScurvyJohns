using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // allows us to change scenes


/// <summary>
/// MenuController Controlls the current active scene.
/// </summary>
public class MenuController : MonoBehaviour
{
    // Scene Indexes
    //---------------
    //  0 - MainMenu
    //  1 - Level1
    //  2 - Level2

    // This loads level 1
    public void loadLevel1 ()
    {
        SceneManager.LoadScene(1);
    }
    // This Loads Level 2 
    public void loadLevel2()
    {
        SceneManager.LoadScene(2);
    }
    // This Loads the main menu
    public void loadMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
