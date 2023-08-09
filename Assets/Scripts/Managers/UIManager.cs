using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class UIManager : MonoBehaviour
    {
        public GameObject debugTextPrefab;

		public static UIManager singleton;

		private void Awake()
		{
			singleton = this;
		}


		private void Update()
		{
			
		}

		public GameObject CreateDebugTextObj()
		{
			GameObject go = Instantiate(debugTextPrefab);
			go.transform.SetParent(debugTextPrefab.transform.parent);
			go.transform.localScale = Vector3.one;

			return go;
		}
	}
}
