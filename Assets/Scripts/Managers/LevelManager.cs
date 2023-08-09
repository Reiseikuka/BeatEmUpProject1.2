using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

namespace SA
{
    public class LevelManager : MonoBehaviour
    {
        public PolyNavMap navmesh;

        public static LevelManager singleton;

        private void Awake()
        {
            singleton = this;
        }
    }
}


