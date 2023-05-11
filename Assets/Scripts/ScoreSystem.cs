using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class ScoreSystem : MonoBehaviour
    {

        public HitCounter hcounter;
        public static ScoreSystem instance;
        public Text ScoreText;

        public int finalhit;
        /*
          The HitCounter Script will keep adding +1 each time the Player hits an enemy.
          When the Player finally get hit or the HitCounter timer reaches 0, that number
          will be stored in the lasthit variable and then will be passed by
          to the ScoreSystem finalhit variable, so we can multiply said number
          accordingly and add it to the Score.  */

        private int multiplier;
        /*Variable that will contain the number of hits done by the player multiplied by the 
          bonus number according to how many hits the Player has done. */

        private int score;
          /*Used as a placeholder variable for*/

        private int currentscore = 0;
        /*The variable that will be used for the Text containing the actual
          score of the player*/

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            ScoreText.text = currentscore.ToString();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            AddPoints();
        }

        public void AddPoints()
        {
            if (finalhit < 10 && finalhit > 0)
            {
                multiplier = finalhit * 2;

                score = multiplier;
            }
            
            if (finalhit < 20 && finalhit > 10)
            {
                multiplier = finalhit * 4;
                
                score = multiplier;
            }

            if (finalhit < 40 && finalhit > 20)
            {
                multiplier = finalhit * 8;
                
                score = multiplier;
            }

            if (finalhit > 40 )
            {
                multiplier = finalhit * 16;
                
                score = multiplier;
            }
            
            currentscore = score;

            ScoreText.text = currentscore.ToString();

        }
    }
}
