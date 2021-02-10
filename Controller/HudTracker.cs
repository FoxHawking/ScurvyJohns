using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Part of the Namespace Observerpattern
namespace ObserverPattern
{

    /// <summary>
    /// HudTracker tracks Heads up display objects, Is a subclass of Observer. Observes Collectibles 
    /// </summary>
    public class HudTracker : Observer
    {
        //Image array, Holds array of images in hud.
        public Image[] objectlist;

        //Serialized sprites, These hold the collected sprite for each type.
        [SerializeField] Sprite Coin;
        [SerializeField] Sprite Bottle;

        // Start is called before the first frame update
        void Start()
        {
            objectlist = GetComponentsInChildren<Image>();
        }

        /// <summary>
        /// On notify Update the object list sprite based on 
        /// </summary>
        /// <param name="Level">Unused (Used by other observers)</param>
        /// <param name="ItemID">Collected Item ID</param>
        public override void OnNotify(int Level, int ItemID)
        {
            //Update the collected hud item 
            if (ItemID < 1)
            {
                objectlist[ItemID].sprite = Coin;
            }
            else if(ItemID == 100)
            {
                //Do nothing, this is the end level state handled by the GameState Observer.
            }
            else
            {
                objectlist[ItemID].sprite = Bottle;
            }
        }

        /// <summary>
        /// Override for Onnotify Containing a boolean, Unused by this observer.
        /// </summary>
        /// <param name="Temp">Unused</param>
        /// <param name="Temp2">Unused</param>
        public override void OnNotify(bool Temp, int Temp2)
        {
            //Do nothing
        }
    }
}
