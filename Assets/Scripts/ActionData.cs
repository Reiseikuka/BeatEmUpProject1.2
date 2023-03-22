using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace SA
    {
        [System.Serializable]
        public class ActionData
        {
            public string actionAnim;
            public DamageType damageType;
            public bool canHitAllies;
            public int damage = 10;
            public bool isDeterministic;
            public bool onHitOverrideMyAnimation;
            public string myOverrideAnimation;
            public float crossfadeTime;
            public InputHandler.InputFrame inputs;
        }

        public enum DamageType
        {
            light, mid, heavy, bounce
            //if bounce, sent them up
        }
        
    }
