using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
/*--------- VARIABLES -------------*/
    private float speed = 3;
    //Movement speed
    public bool attacking;
    //For Light attacks
    public bool hattacking;
    //For Heavy attacks.
    public bool kicking;
    //Will be used for character when jumping so they can kick on the air
   
    public Animator anim;
    //To bring the Animator

    private float Gravity;
    //Gravity will be recreated
    private float Ypos;
    //So we can use the value in Y axis
    private float YposGround;
    //So we can detect the position of the ground in Y axis.
    public bool OnGround;
    //When the Player is touching the Ground
    public bool jumping;
    //For whenever the Player are Jumping
    public int Phases;
    //For the jumping phaces
    public float jumpHeight;
    //We will give a height to the jump done by the Player
    public float jumpPower;
    //To see the power that said jump will have
    public float Fallen;
    //To regulate the falling
    public SpriteRenderer spr;
    //To set the character whenever he is attacking the enemy
    private float delay;
    //Delay for each attack to give opportunity the enemy to fight back
    private int sky_;
    //Are we falling or going up?


    float inputHorizontal;
    float inputVertical;
    Rigidbody2D rb;

/*--------- FUNCTIONS -------------*/
    void Start()
    {
        anim = GetComponent<Animator>();
        //Get the Animator
        spr = GetComponent<SpriteRenderer>();
        //Get the Renderer for the Sprite
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Get the Rigidbody
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !attacking && !jumping && !hattacking && OnGround)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            anim.SetBool("walk", true);
        }else
        {
            anim.SetBool("walk", false);
        }
        /*If we move up and we are not attacking or 
         jumping while on the ground: Activate movement up.*/

        if (Input.GetKey(KeyCode.DownArrow) && !attacking && !jumping && !hattacking && OnGround)
        {
            transform.Translate(Vector3.up * -speed * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        /*If we move down and we are not attacking or 
         jumping while on the ground: Activate movement down.*/


    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && !jumping && OnGround)
        {
            YposGround = transform.position.y;
            jumping = true;
            OnGround = false;
        }

        if (jumping)
        {
            switch(Phases)
            {
                case 0:
                    Gravity = jumpHeight;
                    Phases = 1;

                    break;

                case 1:
                     if (Gravity > 0)
                     {
                        Gravity -= jumpPower * Time.deltaTime;
                     }
                     else
                     {
                        Phases = 2;
                     }

                     break;
            }
        }
    }

    void SetTransform(float n)
    {
        transform.position = new Vector3(transform.position.x, n, transform.position.z);
    }

    public void DetectGround()
    {
        if(!jumping && !attacking && !hattacking)
        {
            sky_ = 0;

            if(Ypos == YposGround)
            {
                OnGround = true;
            }
            anim.SetBool("jump", false);
        }else
        {
            anim.SetBool("jump", true);
        }

        if (OnGround)
        {
            Gravity = 0;
            Phases = 0;
        }
        else
        {
            switch(Phases)
            {
                case 2:
                    Gravity = 0;
                    Phases = 3;

                    break;

                case 3:
                    if (Ypos >= YposGround)
                    {
                        if(Gravity > -10)
                        {
                            Gravity -= jumpHeight / Fallen * Time.deltaTime;
                        }
                    } else
                    {
                        jumping = false;
                        OnGround = true;
                        SetTransform(YposGround);
                        Phases = 0;
                    }

                    break;
            }
        }

        if(!OnGround && !kicking)
        {
            if(transform.position.y > Ypos)
            {
                anim.SetFloat("gravity", 1);
            }
            if (transform.position.y < Ypos)
            {
                anim.SetFloat("gravity", 0);

                switch (sky_)
                {
                    case 0:
                        anim.Play("Base Layer.Jump", 0, 0);
                        sky_++;
                        break;
                }
            }
        }
        Ypos = transform.position.y;
    }

    public void FinishAnim()
    {
        attacking = false;
        hattacking = false;
        kicking = false;
    }

    public void Hitting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            delay =  1;
            if (!jumping)
            {
                if(!attacking)
                {
                    attacking = true;
                    anim.SetTrigger("hit");
                    //if we are on the ground
                }
            } else
            {
                if (!kicking)
                {
                    kicking = true;
                    anim.SetTrigger("kick");
                }
            }
            //If we are jumping, kick in hte air
        }
        //For light attacks

        if (Input.GetKeyDown(KeyCode.X))
        {
            delay =  1;
            if (!jumping)
            {
                if(!hattacking)
                {
                    hattacking = true;
                    anim.SetTrigger("heavyhit");
                    //if we are on the ground
                }
            } else
            {
                if (!kicking)
                {
                    kicking = true;
                    anim.SetTrigger("kick");
                }
            }
            //If we are jumping, kick in the air
        }
        //For heavy attack

        if (delay > 0)
        {
            spr.sortingOrder = 1;
            delay -= 2 * Time.deltaTime;
        } else
        {
            spr.sortingOrder = 0;
            delay = 0;
        }
    }
    void Update()
    {
        DetectGround();
        Jump();
        Hitting();
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
         
        if (inputHorizontal != 0)
        {
            rb.AddForce(new Vector2(inputHorizontal * speed, 0f));
        }

        if (inputHorizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("walk", true);
        }
        if (inputHorizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("walk", true);
        }


        if (Input.GetKey(KeyCode.UpArrow) && !attacking && !jumping && !hattacking && OnGround)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            anim.SetBool("walk", true);
        }else
        {
            anim.SetBool("walk", false);
        }
        /*If we move up and we are not attacking or 
         jumping while on the ground: Activate movement up.*/

        if (Input.GetKey(KeyCode.DownArrow) && !attacking && !jumping && !hattacking && OnGround)
        {
            transform.Translate(Vector3.up * -speed * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        /*If we move down and we are not attacking or 
         jumping while on the ground: Activate movement down.*/

        transform.Translate(Vector3.up * Gravity * Time.deltaTime);
    }
}
