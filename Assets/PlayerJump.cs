using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerJump : MonoBehaviour
{
    public GroundCheck groundCheck;
    public float jumpForce= 20;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;
    void Update()
    {
        velocity += gravity * gravityScale * Time.deltaTime;
        if (groundCheck.isGrounded && velocity < 0)
        {
            float floorHeight = 0.7f;
            velocity = 0;
            transform.position = new Vector3(transform.position.x, groundCheck.surfacePosition.y + floorHeight, transform.position.z);
        }
        /*To snap the object to the floor whenever it’s grounded, I can set its height to the Surface
          Position’s Y value, plus an offset to account for the height of the player.*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = jumpForce;
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
    }

    /*Because the gravity value is not a constant speed, it’s an acceleration, measured in metres per
      second squared. Multiplying gravity by the Delta Time twice squares the time value, returning an
      acceleration of –9.81 m/s2, which can then be added to an object’s existing velocity to create 
      an accelerating gravitational force.

      First, add the gravity. Next, check if the player is grounded. If they are, reset their Velocity,
      which cancels out the gravity, so that they don’t fall through the floor.
      Finally, if the player tries to jump, their new upward Velocity won’t be affected  by being
      grounded.*/
}