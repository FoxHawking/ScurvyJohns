using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Part of the Namespace Observerpattern
namespace ObserverPattern
{
    /// <summary>
    /// Observer Abstract Class definition, contains prototypes for both notify functions 
    /// </summary>
    public abstract class Observer : MonoBehaviour
    {
        public abstract void OnNotify(int Level, int ItemID);
        public abstract void OnNotify(bool PlayerReady, int playerNo);
    }

    /// <summary>
    /// Observable class definition
    /// </summary>
    public class Observable : MonoBehaviour
    {
        
        //List of Observers
        List<Observer> observers = new List<Observer>();

        /// <summary>
        /// Nofify Definition, loops through and notifys observers
        /// </summary>
        /// <param name="PlayerReady">Boolean for Player Ready State</param>
        /// <param name="playerNo">Integer for Player 1 or player 2</param>
        public void Notify(bool PlayerReady, int playerNo)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify(PlayerReady, playerNo);
            }
        }

        /// <summary>
        /// Nofify Definition, loops through and notifys observers
        /// </summary>
        /// <param name="Level">Integer for Level</param>
        /// <param name="CollectibleID">Integer for collectibleID</param>
        public void Notify(int Level, int CollectibleID)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                 observers[i].OnNotify(Level, CollectibleID);
            }
        }

        //Add observer to the observer list
        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }
    }
}


