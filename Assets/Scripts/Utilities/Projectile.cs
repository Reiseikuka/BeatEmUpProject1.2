using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class Projectile : MonoBehaviour
    {
		public float speed = 3;
		public ActionData action;

		private void Update()
		{
			transform.position += transform.right * (Time.deltaTime * speed);
		}
	}
}
