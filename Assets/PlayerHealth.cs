using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //needed for the slider

namespace SA
{
    public class PlayerHealth : MonoBehaviour
    {
        public Slider healthBarSlider;
        //Reference to the Health Bar, made with a Slider

        public UnitController unitcontroller;
        /*Reference to the UnitController of the Player, that 
        has the value of the character health*/

        public int maxHealth; //Max Health of the player.
        public int currentHealth; //Current Health of the `ñauer


        void Start()
        {
            currentHealth = unitcontroller.health;
            maxHealth = unitcontroller.maxhealth;
        }

        void FixedUpdate()
        {
            currentHealth = unitcontroller.health;
            maxHealth = unitcontroller.maxhealth;
        }
        //Whenever Player gets hit or recovers health, it should be shown exactly when it happens

        //Setting the Slider Values
        void Update()
        {
            healthBarSlider.value = currentHealth;
            healthBarSlider.maxValue = maxHealth;
        }
    }
}
