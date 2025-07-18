using UnityEngine;
using Utility;

namespace Items
{
    public class EffectCollisionSpawner : ColliderProvider
    {
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private Transform parent;
        
        private void OnEnable()
        {
            CollisionEnter += EffectSpawn;
        }
        private void OnDisable()
        {
            CollisionEnter -= EffectSpawn;
        }
        
        private void EffectSpawn(Collision other)
        {
            if (other.contacts.Length != 0)
                Instantiate(effectPrefab, other.contacts[0].point, Quaternion.identity, parent);
        }
    }
}