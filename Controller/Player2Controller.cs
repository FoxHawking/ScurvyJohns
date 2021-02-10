using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    //Rigid body and box colliders
    new Rigidbody2D body = null;
    new BoxCollider2D collider = null;

    //Private variables
    private Animator anim;  //animaation
    private Vector2 Respawn; //respawn
    private int DeathCount = GameStats.lev1Deaths; //death count
    private DateTime WallTimer; //wall jump timer
    private DateTime DeathTimer; //Death timer

    //Serialized fields
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float MaxSpeed = 5f;
    [SerializeField] float Speed = .75f;
    [SerializeField] float ArialDivisor = 2f;
    [SerializeField] Text DeathText;
    [SerializeField] Text Stats;
    [SerializeField] int WallJumps = 1;
    [SerializeField] float Deceleration = .1f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        Respawn = transform.position;
        DeathText.text = DeathCount.ToString();
        Deceleration = Deceleration + 1;
    }

    /// <summary>
    /// Set death text
    /// </summary>
    /// <param name="Death">Death text</param>
    public void SetDeath(Text Death)
    {
        DeathText = Death;
    }

    // Update is called once per frame
    void Update()
    {
        //Booleans for Layers touching
        bool Ground = collider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool Wall = collider.IsTouchingLayers(LayerMask.GetMask("Wall"));
        bool Death = collider.IsTouchingLayers(LayerMask.GetMask("Death"));

        //Jumping Animation ----------------------------------------------------------------------------------------------------
        // Stops jump animations (but doesn't work really - consider revising)
        if (Ground || Wall)
        {
            anim.SetBool("isJumping", false);
        }

        // If standing still
        if ((body.velocity.x < 0.2 || body.velocity.x > -0.2) || !Ground)
        {
            anim.SetBool("isRunning", false);
            if (!Ground)
            {
                anim.SetBool("isJumping", true);
            }
        }
        // If moving
        if ((body.velocity.x > 0.2 || body.velocity.x < -0.2) && Ground)
        {
            anim.SetBool("isRunning", true);
        }
        // =======================================================================================================================



        // Key input For Ground/Wall Control -------------------------------------------------------------------------------------
        if ((Input.GetKey(KeyCode.RightArrow)) && (Ground || Wall))
        {
            //Face the correct direction
            transform.localScale = new Vector2(-1, 1);

            //Increase speed if not at Maxspeed.
            if (body.velocity.x <= MaxSpeed)
            {
                body.velocity = new Vector3((body.velocity.x + Speed), body.velocity.y);
            }

            //Slide down wall if no jumps left
            if (Wall && WallJumps <= 0 && (DateTime.Now - WallTimer).TotalMilliseconds > 100)
            {
                body.velocity = new Vector3(body.velocity.x, -1.5f, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.RightShift)) //(jump action)
            {
                //if Touching ground
                if (Ground)
                {
                    body.velocity = new Vector3(body.velocity.x, jumpPower, 0f);
                }
                //Wall jump if jumps left
                else if (Wall && WallJumps > 0)
                {
                    WallTimer = DateTime.Now; // Reset time diff
                    body.velocity = new Vector3(body.velocity.x - jumpPower / ArialDivisor, jumpPower, 0f);
                    WallJumps--;
                }
            }
        }
        //Key input for left arrow
        else if ((Input.GetKey(KeyCode.LeftArrow)) && (Ground || Wall))
        {
            //face correct direction
            transform.localScale = new Vector2(1, 1);

            //Increase speed if not at -Maxspeed
            if (body.velocity.x >= -MaxSpeed)
            {
                body.velocity = new Vector3((body.velocity.x - Speed), body.velocity.y);
            }

            //slide down wall if no jumps left
            if (Wall && WallJumps <= 0 && (DateTime.Now - WallTimer).TotalMilliseconds > 100)
            {
                body.velocity = new Vector3(body.velocity.x, -1.5f, 0f);
            }

            //action (Jump)
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                //if touching ground
                if (Ground)
                {
                    body.velocity = new Vector3(body.velocity.x, jumpPower, 0f);
                }
                //Wall jump if jumps left
                else if (Wall && WallJumps > 0)
                {
                    WallTimer = DateTime.Now; //rest time diff
                    body.velocity = new Vector3(body.velocity.x + jumpPower / ArialDivisor, jumpPower, 0f);
                    WallJumps--;
                }
            }
        }

        // =========================================================================================================



        //Key input for arial Controls -------------------------------------------------------------------------------
        if ((Input.GetKey(KeyCode.RightArrow)) && (!Ground))
        {
            if (body.velocity.x <= MaxSpeed)
            {
                body.velocity = new Vector3(body.velocity.x + Speed / ArialDivisor, body.velocity.y);
            }
        }
        else if ((Input.GetKey(KeyCode.LeftArrow)) && (!Ground))
        {
            if (body.velocity.x >= -MaxSpeed)
            {
                body.velocity = new Vector3(body.velocity.x - Speed / ArialDivisor, body.velocity.y);
            }
        }

        if (Ground)
        {
            if (WallJumps <= 0)
            {
                WallJumps = 1;
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                body.velocity = new Vector3(body.velocity.x, jumpPower, 0f);
            }
        }// ===========================================================================================================


        //Death Loop, cant occur twice quickly 
        if (Death && (DateTime.Now - DeathTimer).TotalMilliseconds > 50)
        {
            //increases deathcount, resets death timer, plays audio and updates text
            DeathCount = Int32.Parse(DeathText.text);
            DeathTimer = DateTime.Now;
            transform.position = Respawn;
            GetComponentInChildren<AudioSource>().Play();
            DeathCount++;
            DeathText.text = DeathCount.ToString();
            GameStats.lev1Deaths = DeathCount;
        }
    }

}
