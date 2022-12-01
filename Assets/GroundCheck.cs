using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public float offset = 0.1f;
    public Vector2 surfacePosition;
    ContactFilter2D filter;
    //It's a custom data set that can be used to ignore certain types of Collider.
    Collider2D[] results = new Collider2D[1];
    /* We are interested in getting information about one Collider at a time,
       so let's set the length of the Array at One. If the number of Colliders 
       returned is zero, the player is not grounded, meaning they’re in the air.*/

    private void Update()
    {
        Vector2 point = transform.position + Vector3.down * offset;
        Vector2 size = new Vector2(transform.localScale.x, transform.localScale.y);
        if (Physics2D.OverlapBox(point, size, 0, filter.NoFilter(), results) > 0)
        {
            /* Overlap Box method do work for checking if the ground is beneath the player. How? By
               checking if a Collider falls within a box area. The box is defined by its center
               coordinate in world space and by its size.*/

            isGrounded = true;
            surfacePosition = Physics2D.ClosestPoint(transform.position, results[0]);
            /*This function provides the ability to calculate the closest point of a specified
             position to the perimeter of any Collider2D type.*/
        }
        else 
        {
            isGrounded = false;
        }
    }

    /*A Ground Check help us test whether or not a player object is currently grounded, 
      meaning that they’re touching the floor.

      Ground Check will allows us to restrict the player from performing
      actions in the air that should, normally, only happen when the player is 
      standing on the ground.

      Actually, every method of jumping will usually need some sort of ground check as, without it,
      players would be able to infinitely jump without first needing to land.*/
}
