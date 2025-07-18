using System;
using UnityEngine;
using Utility;

namespace Items
{
    public class BulletDestroyProvider : ColliderProvider
    {
        [SerializeField] private GameObject bulletObject;

        private void Awake()
        {
            bulletObject ??= gameObject;
        }
        private void OnEnable()
        {
            CollisionEnter += BulletEnter;
        }
        private void OnDisable()
        {
            CollisionEnter -= BulletEnter;
        }
        
        private void BulletEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}