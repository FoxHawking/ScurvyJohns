using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class holds all of the player stats, from currently selected pirate,
/// to number of deaths per level, to number of collectibles collected.
/// </summary>
public class GameStats : MonoBehaviour
{

    // Checks for Multiple Gamestatemanagers and deletes any new instances of it, Retaining original.
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameStats");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    // Character index for player1 and player2 selected pirate
    static public int p1Pirate = 3;
    static public int p2Pirate = 3;

    // Level 1 deaths data
    static public int lev1Deaths = 0;

    // Level 2 deaths data
    static public int lev2Deaths = 0;

    // Bottles array to store collected bottles
    static public bool[,] Bottles = new bool[,] { { false, false, false },
                                                  { false, false, false } };

    //Coin Array to store collected coins
    static public bool[] Coin = new bool[] { false, false };

    //boolean to store Co-op state
    static public bool Coop = false;
}

