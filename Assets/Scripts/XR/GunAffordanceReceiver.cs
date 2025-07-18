using System;
using System.Collections;
using Items;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using Utility;
using XR.AffordanceSystem;

namespace XR
{
    public class GunAffordanceReceiver : MonoBehaviour
    {
        [SerializeField] private FloatAffordanceReceiver receiver;
        [SerializeField] private GunConfig config;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private Transform bulletParent;
        
        [ReadOnly] private bool isActive;
        private CoroutineWrapper coroutineWrapper;

        private void Awake()
        {
            coroutineWrapper = new CoroutineWrapper(this);
        }
        private void Start()
        {
            //coroutineWrapper.Start(MainCycle());
        }
        private void OnDestroy()
        {
            coroutineWrapper.Stop();
        }

        private void OnEnable()
        {
            receiver.valueUpdated.AddListener(ValueChanged);
        }
        private void OnDisable()
        {
            receiver.valueUpdated.RemoveListener(ValueChanged);
        }
        private void ValueChanged(float value)
        {
            if (!coroutineWrapper.IsRunning() && value >= 1)
                coroutineWrapper.Start(MainCycle());
            else if (coroutineWrapper.IsRunning() && value <= 0)
                coroutineWrapper.Stop();
        }

        private IEnumerator MainCycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(config.rate);
                var bullet = Instantiate(config.bulletPrefab, bulletPoint.position, bulletPoint.rotation, bulletParent);
                if (bullet.TryGetComponent<Rigidbody>(out var rbBullet))
                {
                    var force = bulletPoint.forward * config.bulletSpeed;
                    rbBullet.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }
}