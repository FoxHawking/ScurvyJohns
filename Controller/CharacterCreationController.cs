using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverPattern
{

    /// <summary>
    /// CharacterCreationController, Observable Class. Handles character creation.
    /// </summary>
    public class CharacterCreationController : Observable
    {
        //Serilized Game state (to attach unity gamestate object)
        [SerializeField] Observer GameState;

        [SerializeField] bool Coop;

        //default pirate index
        [SerializeField] int pirate;

        //Create new gamestats object
        [SerializeField] GameStats GameStats;

        // Set up all the body parts of the pirate on the pedistal
        public Image head;
        public Image body;
        public Image barm;
        public Image farm;
        public Image bleg;
        public Image fleg;

        // Set up the sprites for the red pirate
        public Sprite r_head;
        public Sprite r_body;
        public Sprite r_barm;
        public Sprite r_farm;
        public Sprite r_bleg;
        public Sprite r_fleg;

        // Set up the sprites for the blue pirate
        public Sprite b_head;
        public Sprite b_body;
        public Sprite b_barm;
        public Sprite b_farm;
        public Sprite b_bleg;
        public Sprite b_fleg;

        // Set up the sprites for the yellow pirate
        public Sprite y_head;
        public Sprite y_body;
        public Sprite y_barm;
        public Sprite y_farm;
        public Sprite y_bleg;
        public Sprite y_fleg;

        // Set up the sprites for the green pirate
        public Sprite g_head;
        public Sprite g_body;
        public Sprite g_barm;
        public Sprite g_farm;
        public Sprite g_bleg;
        public Sprite g_fleg;

        // Bounds for the left and right arrows
        private int maxpirates = 5;
        private int minpirates = 0;

        // Pirate Codes
        //--------------
        // 1 = Red
        // 2 = Blue
        // 3 = Yellow
        // 4 = Green

        //Start function adds observers.
        void Start()
        {
             AddObserver(GameState);
        }

        // Update is called once per frame
        void Update()
        {
            // If red pirate is selected
            if (pirate == 1)
            {
                head.sprite = r_head;
                body.sprite = r_body;
                barm.sprite = r_barm;
                farm.sprite = r_farm;
                bleg.sprite = r_bleg;
                fleg.sprite = r_fleg;
            }
            // If blue pirate is selected
            else if (pirate == 2)
            {
                head.sprite = b_head;
                body.sprite = b_body;
                barm.sprite = b_barm;
                farm.sprite = b_farm;
                bleg.sprite = b_bleg;
                fleg.sprite = b_fleg;
            }
            // If yellow pirate is selected
            else if (pirate == 3)
            {
                head.sprite = y_head;
                body.sprite = y_body;
                barm.sprite = y_barm;
                farm.sprite = y_farm;
                bleg.sprite = y_bleg;
                fleg.sprite = y_fleg;
            }
            // If green pirate is selected
            else if (pirate == 4)
            {
                head.sprite = g_head;
                body.sprite = g_body;
                barm.sprite = g_barm;
                farm.sprite = g_farm;
                bleg.sprite = g_bleg;
                fleg.sprite = g_fleg;
            }
        }

        /// <summary>
        /// Handles left arrow pressed
        /// </summary>
        public void clickLeft()
        {
            // Decrement pirate
            pirate--;
            // If we run out of pirates going left, go to end of list
            if (pirate == minpirates)
            {
                pirate = maxpirates - 1;
            }
        }

        /// <summary>
        /// Handles Right arrow is pressed
        /// </summary>
        public void clickRight()
        {
            // Increment pirate
            pirate++;
            // If we run out of pirates going right, start at beginning of list
            if (pirate == maxpirates)
            {
                pirate = minpirates + 1;
            }
        }

        /// <summary>
        ///  Confirm selection for pirate, Updates gameStats Pirate selection, and the back button
        /// </summary>
        public void confirm()
        {
            // Find out if we've been manipulating player 1 or player 2
            if (this.name == "1P Stuff")
            {
                //update player 1's pirate in GameStats, Set Gamestats coop Based on Serialized coop.
                GameStats.p1Pirate = pirate;
                GameStats.Coop = Coop;
                //Notify observer that P1 is ready
                Notify(true, 1); 
            }
            else if (this.name == "2P Stuff")
            {

                //We're player 2, so update player 2's pirate in GameStats
                GameStats.p2Pirate = pirate;
                GameStats.Coop = Coop;
                //Notify observer that P2 is ready
                Notify(true, 2);
            }
            else
            {
                //Notify When Back Button is pressed.
                Notify(false, 3);
            }
        }
    }

}
