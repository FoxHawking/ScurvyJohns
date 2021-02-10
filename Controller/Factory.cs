using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface for Factory
public interface IFactory
{
    GameObject getNewInstance(string ResourceName, GameObject player);
}

/// <summary>
/// //Factory Class Builds Pirates from Prefabs, Implements Ifactory Interface.
/// </summary>
public class Factory : MonoBehaviour, IFactory
{

    //Awake Checks for Multiple Factories, Deletes this instance if an instance exists.
    void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Factory");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Build pirate Calls GetNewInstance with the Asset name from the Resource folder
    /// Based on the selected pirate for the given player.
    /// </summary>
    /// <param name="pirate">Index For Pirate Type</param>
    /// <param name="player">Player Game Object</param>
    /// <returns>New Pirate Game object</returns>
    public GameObject buildPirate(int pirate, GameObject player)
    {
        
        // Determine the pirate that was selected
        if (pirate == 1)
        {
            // Building a red pirate
            return getNewInstance("Red", player);
        }
        else if (pirate == 2)
        {
            // Building a blue pirate
            return getNewInstance("Blue", player);
        }
        else if (pirate == 3)
        {
            // Building a yellow pirate
            return getNewInstance("Yellow", player);
        }
        else if (pirate == 4)
        {
            // Building a green pirate
            return getNewInstance("Green", player);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Creates new instance of Pirate with selected prefab and returns it.
    /// </summary>
    /// <param name="Pirate">String of Pirate Asset in Resource Folder</param>
    /// <param name="player">Player Game Object</param>
    /// <returns></returns>
    public GameObject getNewInstance(string Pirate, GameObject player)
    {
        //Load Prefab from resource folder, based on passed in string.
        GameObject NewPirate = Instantiate(Resources.Load<GameObject>(Pirate), player.transform.position, player.transform.rotation);
        Destroy(player);
        return NewPirate;

    }
}




