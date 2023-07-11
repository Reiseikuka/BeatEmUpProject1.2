using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
	public class DebugTextHandler : MonoBehaviour
	{
		public Transform target;
		public Text text;

		RectTransform rectTrans;
		private void Start()
		{
			rectTrans = GetComponent<RectTransform>();
		}

		private void Update()
		{
			if (target == null)
				return;
			
			Vector2 tp = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
			transform.position = tp;
		
		}

	}
}