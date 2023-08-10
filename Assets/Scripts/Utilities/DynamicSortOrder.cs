using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	public class DynamicSortOrder : MonoBehaviour
	{
		public SpriteRenderer spriteRenderer;
		public Transform origin;
		public float yOffset;

		private void Update()
		{
			Vector3 position = transform.position;
			if (origin != null)
			{
				position = origin.position;
			}

			float y = position.y;
			y += yOffset;

			int sortOrder = Mathf.RoundToInt(y * 100) + 10;
			spriteRenderer.sortingOrder = -sortOrder;
		}
	}
}
