using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu]
    public class ActionDataHolder : ScriptableObject
    {
        public ActionsContainer[] actions;

        public ActionsContainer GetActions(int index)
        {
            return actions[index];
        }
    }
    [System.Serializable]
    public class ActionsContainer
    {
        public string actionsId;
        public bool canDash;
        public ActionData[] actions;
    }
}

