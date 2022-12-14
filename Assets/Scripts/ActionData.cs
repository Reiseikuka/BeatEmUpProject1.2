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
        }

        public enum DamageType
        {
            light, mid, heavy
        }
        
    }
