using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public class CoroutineWrapper//<TMonoBehaviour> where TMonoBehaviour : MonoBehaviour
    {
        private readonly MonoBehaviour behaviour;
        private Coroutine coroutine;

        public CoroutineWrapper(MonoBehaviour behaviour)
        {
            this.behaviour = behaviour;
        }

        public void Start(IEnumerator routine)
        {
            if (coroutine != null)
                StopImpl();
            StartImpl(routine);
        }
        public void Stop()
        {
            if (coroutine == null) return;
            StopImpl();
        }

        private void StartImpl(IEnumerator routine)
        {
            coroutine = behaviour.StartCoroutine(routine);
        }
        private void StopImpl()
        {
            behaviour.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}