using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parallax class, handles parallax background behaviour
/// </summary>
public class Parallax : MonoBehaviour
{

    //Parallax Variables
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float d = (cam.transform.position.x * parallaxEffect);

        // Follow the camera with the background pieces
        transform.position = new Vector2(startpos + d, transform.position.y);

        // If we get out of bounds, repeat the background piece
        if (temp > (startpos + length))
        {
            startpos += length;
        }
        else if (temp < (startpos - length))
        {
            startpos -= length;
        }
    }
}
