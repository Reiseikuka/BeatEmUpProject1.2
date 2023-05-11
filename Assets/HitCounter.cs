using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class HitCounter : MonoBehaviour
    {
        [SerializeField] private UnitController player;
        //Reference to the Player of a particular Score Placeholder

        [SerializeField] private DamageCollider dcollider;
        //Reference to DamageCollider script so we can use its bool IsAHit
        private Text Countertext;
        //reference to the Text that would for the counter of hits done by the player

        private int hitCount = 0;
        //Counter for how many enemies the Player has done an attack

        public int lasthit;
        //The number of the last hit made by the Player to an Enemy, will be used for the Score

        public ScoreSystem PlayerScore;
        //Reference to the Player Score Script.

        private void Awake()
        {
            Countertext = GetComponent<Text>();
            //Get the Text Component 
            HideHitCounter();

        }

        private void FixedUpdate()
        {
            EnemyGotHit();
            PlayerGetsHurt();
        }

        private void EnemyGotHit()
        {
            if (dcollider.IsAHit == true)
            {
                hitCount++;
                SetHitCounter(hitCount);
            }
        }
        /*On DamageCollider, if the Player hits an Enemy, it would turn true to the bool
          IsAHit. By being true, this would increase the hitCount and this number will be
          updated on the Text refering to the Player Hit Counter*/

        private void SetHitCounter(int hitCount)
        {
            Countertext.text =  hitCount.ToString();
            //Change the text of the Counter component 
            Countertext.enabled = true;
            dcollider.IsAHit = false;
        }

        private void HideHitCounter()
        {
            Countertext.enabled = false;
        }

        private void PlayerGetsHurt()
        {
            if (player.PlayerHurtdetector == true)
            {
                lasthit = hitCount;
                PlayerScore.finalhit = lasthit;
                /*
                The HitCounter Script will keep adding +1 each time the Player hits an enemy.
                When the Player finally get hit or the HitCounter timer reaches 0, that number
                will be stored in the lasthit variable from Score System, so we can multiply said number
                accordingly and add it to the Score on the Score System script.  */

                hitCount = 0;
                lasthit = 0;
                HideHitCounter();
                player.PlayerHurtdetector = false;
            }
        }

    }

}
