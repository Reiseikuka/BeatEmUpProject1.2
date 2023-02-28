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
        }

        public enum DamageType
        {
            light, mid, heavy
        }
        
    }
