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

        private void Awake()
        {
            Countertext = GetComponent<Text>();
            //Get the Text Component 
            HideHitCounter();

        }

        private void FixedUpdate()
        {
            EnemyGotHit();
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

    }

}
