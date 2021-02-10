using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Part of the Namespace Observerpattern
namespace ObserverPattern
{
    /// <summary>
    /// PirateGameState Class, Contains all the information on the current game state
    /// is a subclass of observer, observes Collectibles, back buttons, and Character selection Ok buttons.
    /// </summary>
    public class PirateGameState : Observer
    {
        //New Factory instance
        Factory Fact = new Factory();

        //Serialized fields, Contain all game objects necassary for interactions

        //Camera
        [SerializeField] Cinemachine.CinemachineVirtualCamera Player1Camera;

        //Players
        [SerializeField] GameObject Player1;
        [SerializeField] GameObject Player2;

        //Menus
        [SerializeField] GameObject Solo;
        [SerializeField] GameObject Coop;
        [SerializeField] GameObject LevelSel;

        //Hud item for death count
        [SerializeField] Text Death;

        //Gameobject for levels
        [SerializeField] GameObject Level1;
        [SerializeField] GameObject Level2; //Level2 - is bound to our test level

        //Game objects for level 1 collection items (displayed in the level select)
        [SerializeField] GameObject L1Star;
        [SerializeField] GameObject L1Coin;
        [SerializeField] GameObject L1Bottle1;
        [SerializeField] GameObject L1Bottle2;
        [SerializeField] GameObject L1Bottle3;
        [SerializeField] Text L1Deaths;

        //Game objects for level 2 collection items (displayed in the level select)
        [SerializeField] GameObject L2Star;
        [SerializeField] GameObject L2Coin;
        [SerializeField] GameObject L2Bottle1;
        [SerializeField] GameObject L2Bottle2;
        [SerializeField] GameObject L2Bottle3;

        //Vectors for each player spawn
        private Vector2 P1Spawn;
        private Vector2 P2Spawn;

        //Player ready states for character selection
        private bool P1ready = false;
        private bool P2ready = false;

        //Create GameStats instance
        [SerializeField] GameStats GameStats;

        //Checks for Multiple Gamestatemanagers and deletes any new instances of it, Retaining original.
        void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("GameStateManager");
            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }


        // OnSceneLoaded, handles scene change to level
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //if level 1
            if (scene.buildIndex == 1)
            {
                //Get players
                Player1 = GameObject.Find("Player1");
                Player2 = GameObject.Find("Player2");
                //Set camera
                SetCameraToForwardPlayer();
                if (GameStats.Coop)
                {
                    //If co-op set spawns to each current player
                    P1Spawn = Player1.transform.position;
                    P2Spawn = Player2.transform.position;
                    //build each players desired pirate
                    UpdatePirate(Fact.buildPirate(GameStats.p1Pirate, Player1), 1);
                    UpdatePirate(Fact.buildPirate(GameStats.p2Pirate, Player2), 2);
                    //Set new pirates spawn location
                    Player1.transform.position = P1Spawn;
                    Player2.transform.position = P2Spawn;

                }
                else
                {
                    //If not co-op set spawn and build desired pirate
                    P1Spawn = Player1.transform.position;
                    UpdatePirate(Fact.buildPirate(GameStats.p1Pirate, Player1), 1);
                    Player1.transform.position = P1Spawn;
                }
            }
            if (scene.buildIndex == 0)
            {
                updateMenu();
            }

        }

        //update runs each frame
        void Update()
        {
            //Sets camera to front player
            if (GameStats.Coop)
            {
                SetCameraToForwardPlayer();
            }
        }


        void OnEnable()
        {
            //Start listening for scene loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            //Stop Listening for scene loaded
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// onNotify sets the collectible collected in the gamestats, also detects when end state is reached
        /// </summary>
        /// <param name="Level">Level ID</param>
        /// <param name="ItemID">Item ID</param>
        public override void OnNotify(int Level, int ItemID)
        {
            //set collectible
            if (ItemID < 1)
            {
                GameStats.Coin[Level] = true;
            }
            //end state
            else if (ItemID == 100)
            {
                SceneManager.LoadScene(0); //change to main menu
            }
            else
            {
                //set collectible
                GameStats.Bottles[Level, ItemID - 1] = true;
            }
        }

        /// <summary>
        /// Updates the level select Collectible Display
        /// </summary>
        void updateMenu()
        {
            if (GameStats.Coin[0] == true)
            {
                L1Coin.SetActive(true);
            }
            if (GameStats.Coin[1] == true)
            {
                L2Coin.SetActive(true);
            }
            if (GameStats.Bottles[0, 0] == true)
            {
                L1Bottle1.SetActive(true);
            }
            if (GameStats.Bottles[0, 1] == true)
            {
                L1Bottle2.SetActive(true);
            }
            if (GameStats.Bottles[0, 2] == true)
            {
                L1Bottle3.SetActive(true);
            }
            if (GameStats.Bottles[1, 0] == true)
            {
                L2Bottle1.SetActive(true);
            }
            if (GameStats.Bottles[1, 1] == true)
            {
                L2Bottle2.SetActive(true);
            }
            if (GameStats.Bottles[1, 2] == true)
            {
                L2Bottle3.SetActive(true);
            }
            if (GameStats.Coin[0] == true && GameStats.Bottles[0, 0] == true && GameStats.Bottles[0, 1] == true && GameStats.Bottles[0, 2] == true)
            {
                L1Star.SetActive(true);
            }
            if (GameStats.lev1Deaths > 0)
            {
                L1Deaths.text = GameStats.lev1Deaths.ToString();
            }
        }

        /// <summary>
        /// OnNotify for player ready states, (ok button presses), and back button presses.
        /// </summary>
        /// <param name="PlayerReady">Player ready boolean</param>
        /// <param name="PlayerNo">Player number integer</param>
        public override void OnNotify(bool PlayerReady, int PlayerNo)
        {
            //if player 1
            if (PlayerNo == 1)
            {
                P1ready = PlayerReady;
            }
            //if player 2
            else if (PlayerNo == 2)
            {
                P2ready = PlayerReady;
            }
            //if back button pressed
            else if (PlayerNo == 3)
            {
                //playerReady will always be false when back button pressed
                P1ready = PlayerReady;
                P2ready = PlayerReady;
            }

            //Occurs if both players are ready, changes menu selection from coop to level select
            if (P1ready == true && P2ready == true && GameStats.Coop)
            {
                Coop.SetActive(false);
                LevelSel.SetActive(true);
            }
        }

        /// <summary>
        /// Update pirate deletes the current pirate and replaces it with passed in new pirate.
        /// </summary>
        /// <param name="Pirate">new pirate object</param>
        /// <param name="Num">player number</param>
        public void UpdatePirate(GameObject Pirate, int Num)
        {
            //update player 1
            if (Num == 1)
            {
                Destroy(Player1);
                Player1 = Pirate;
                var SCRIPT = Player1.AddComponent<Player1Controller>();
                SCRIPT.SetDeath(Death);
            }
            //update player 2
            else if (Num == 2)
            {
                Destroy(Player2);
                Player2 = Pirate;
                var SCRIPT = Player2.AddComponent<Player2Controller>();
                SCRIPT.SetDeath(Death);
            }
            //if coop is false destroy player 2
            if (!GameStats.Coop)
            {
                Destroy(Player2);
            }
            //set camera
            SetCameraToForwardPlayer();
        }

        /// <summary>
        /// SetCameraToForwardPlayer assigns the cameras follow target to the player closest to the end goal
        /// </summary>
        void SetCameraToForwardPlayer()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (Player1.transform.position.x < Player2.transform.position.x && GameStats.Coop == true)
                {
                    Player1Camera.Follow = Player2.transform;
                }
                else
                {
                    Player1Camera.Follow = Player1.transform;
                }
            }
        }
    }
}

