using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utility
{
    public class SelfDestroy : MonoBehaviour
    {
        public float lifeTime = 5f;
        public bool onStart = true;

        private void Start()
        {
            if (onStart)
                Destroy();
        }
        public void Destroy()
        {
            Object.Destroy(this, lifeTime);
        }
    }
}