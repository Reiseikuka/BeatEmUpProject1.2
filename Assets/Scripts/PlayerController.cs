using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components

    Rigidbody2D rb;
    //Player
    private float speed = 3f;
    //Walk Speed of the character
    private float speedLimiter = 0.7f;
    //Limiter to our vertical movement
    private float inputHorizontal;
    //So we can set the movement from right to left
    private float inputVertical;
    //So we can set the movement from up to down

    //Animations
    public Animator anim;
    //To bring the Animator
    bool facingRight = true;

    //Attacking
    public bool attacking;
    //For Light attacks
    public bool hattacking;
    //For Heavy attacks.

    //Movement
    bool isMoving = false;
    bool canMove = true;
//sasa
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Get the Rigidbody, which involves anything physics related

        anim = GetComponent<Animator>();
        //Get the Animator
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        //Grab the input of the Keyboard to move horizontally
        inputVertical = Input.GetAxisRaw("Vertical");         
        //Grab the input of the Keyboard to move vertically

        //Functions
        Attacks();
        Boundaries();
    }

    void FixedUpdate()
    {
        Movement();
        Flip();
        
        //Debug.Log(Camera.main.transform.position);
        //Check what the camera is doing
    }

    private void Movement()
    {
        if (canMove == true && inputHorizontal != 0 || inputVertical != 0) 
        {
            if(inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
                /*If the player is going in diagonal direction,
                  limit the speed of the character so the speed
                  doesn't get in double value due the Player
                  pressing two buttons at the same time.*/
            }

            rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);
            anim.SetBool("walk", true);
            isMoving = true;
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
            anim.SetBool("walk", false);  
            isMoving = false;         
        }
        /*When the Player press the input, make the object of the 
          Player move. Once the Player releases the finger of the button,
          the object should stop. */

        /*We are using velocity because we want the Player
          to stop immediatly when we are not pressing the
          inputs.*/      
    }

    private void Flip()
    {
        if (inputHorizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (inputHorizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    /*Change the Character animation from  Right to Left and viceversa*/

    public void Attacks()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!attacking)
            {
                attacking = true;
                anim.SetTrigger("hit");
                Debug.Log("Z button is pressed");
            }else
            {
                attacking = false;
            }
        }
        //For light attacks

        if (Input.GetKeyDown(KeyCode.X))
        {
                if(!hattacking)
                {
                    hattacking = true;
                    anim.SetTrigger("heavyhit");
                Debug.Log("X button is pressed");
                }
                else                
                {
                        hattacking = false;
                }
        } 
            //If we are jumping, kick in the air
    }
    //For heavy attack

    void LockMovement()
    {
        canMove = false;
        rb.velocity = new Vector2(0f, 0f);
        anim.SetBool("walk", false);  
        isMoving = false;     
    }

    void UnlockMovement()
    {
        canMove = true;
        rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);
        anim.SetBool("walk", true);
        isMoving = true;
    }
//<>
    void Boundaries()
    {
        //Y AXIS
        if(transform.position.y >= 0.62f)
        {
            transform.position = new Vector3(transform.position.x, 0.62f, 0);
        } else if(transform.position.y <= -0.68f)
        {
            transform.position = new Vector3(transform.position.x, -0.68f, 0);
        }

        //X AXIS

        if(transform.position.x >= 14.23f)
        {
            transform.position = new Vector3(14.23f, transform.position.y, 0);
        } else if(transform.position.x <= -15.34f)
        {
            transform.position = new Vector3(-15.34f, transform.position.y, 0);
        }
    }
}
