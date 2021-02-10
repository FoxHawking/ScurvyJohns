using UnityEngine;
using System.Collections;

//Quits Game on Escape at menu
public class ExitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}