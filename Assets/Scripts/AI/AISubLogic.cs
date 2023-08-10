using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public abstract class AISubLogic : ScriptableObject
    {
        public bool instanceSublogic;

        public abstract void Tick(float delta, AIHandler h);
    }
}