using System.Collections;
using Items;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using XR.AffordanceSystem;

namespace XR
{
    public class GunAffordanceReceiver : BoolAffordanceReceiver
    {
        [SerializeField] private GunConfig config;
        [SerializeField] private Transform bulletPoint;
        
        [ReadOnly] private bool isActive;
        private CoroutineWrapper coroutineWrapper;

        protected override void Start()
        {
            base.Start();
            coroutineWrapper.Start(MainCycle());
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            coroutineWrapper.Stop();
        }

        /// <inheritdoc/>
        protected override void OnAffordanceValueUpdated(bool newValue)
        {
            isActive = newValue;
            base.OnAffordanceValueUpdated(newValue);
        }
        /// <inheritdoc/>
        protected override bool GetCurrentValueForCapture()
        {
            return isActive;
        }
        
        private IEnumerator MainCycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(config.rate);
                var bullet = Instantiate(config.bulletPrefab, Vector3.zero, Quaternion.identity, bulletPoint);
                if (bullet.TryGetComponent<Rigidbody>(out var rbBullet))
                    rbBullet.AddForce(config.bulletSpeed, 0, 0, ForceMode.Impulse);
            }
        }
        
    }
}