using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SA
{
    public class OnTriggerEvent : MonoBehaviour
    {
        public UnityEvent  myEvent;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var inp = collision.transform.GetComponentInParent<UnitController>();

            if (inp != null)
            {
                if (!inp.isAI)
                {
                    myEvent.Invoke();
                }
            }

        }

    }
}

