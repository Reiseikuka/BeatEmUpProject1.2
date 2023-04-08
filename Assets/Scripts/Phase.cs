using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SA
{
    public class Phase : MonoBehaviour
    {
        public List<AIHandler> currentUnits = new List<AIHandler>();
        //List of enemies that are going to be part of a Phase

        public UnityEvent onPhaseStart;
        public UnityEvent onPhaseEnded;

        public void RegisterUnit(AIHandler a)
        {
            currentUnits.Add(a);
        }

        public void UnRegisterUnit(AIHandler a)
        {
            currentUnits.Remove(a);

            Debug.Log("un");

            if (currentUnits.Count == 0)
            {
                onPhaseEnded.Invoke();
            }
            /**Once the enemies of an area has been defeated,
               you can keep going through the rest of the area*/
        }
    }
}

