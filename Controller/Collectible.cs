using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part of the Namespace Observerpattern
namespace ObserverPattern
{
    /// <summary> 
    /// Collectible Class, Attached to collectible Objects. Notify all observers when collided with, then sets the object inactive.
    /// Collectible is a subclass of Observable.
    /// </summary>
    public class Collectible : Observable
    {
        //Serialized Fields, Sent from each game object
        [SerializeField] int CollectibleID;
        [SerializeField] int Level;
        [SerializeField] Observer Hud;
        [SerializeField] Observer GameState;

        //Start function Adds observers to the observables observer list.
        void Start()
        {
            AddObserver(Hud);
            AddObserver(GameState);
        }

        //On Collision with player notify observers and set current object to false.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //check for collision.
            if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2" 
                || collision.gameObject.name == "Red(Clone)" || collision.gameObject.name == "Green(Clone)"
                || collision.gameObject.name == "Blue(Clone)" || collision.gameObject.name == "Yellow(Clone)")
            {
                //Send notification to observers, mark object as inactive.
                Notify(Level, CollectibleID);
                gameObject.SetActive(false);
            }
        }

    }
}
