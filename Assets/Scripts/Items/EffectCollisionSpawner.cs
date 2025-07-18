using UnityEngine;
using Utility;

namespace Items
{
    public class EffectCollisionSpawner : ColliderProvider
    {
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private Transform spawnPoint;
        
        private void Awake()
        {
            spawnPoint ??= transform;
        }
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
            Instantiate(effectPrefab, spawnPoint);
        }
    }
}