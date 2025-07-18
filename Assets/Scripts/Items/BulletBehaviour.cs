using UnityEngine;

namespace Items
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private BulletConfig config;

        private void Start()
        {
            SpawnEffect(config.bulletStartEffect);
        }
        private void OnDestroy()
        {
            SpawnEffect(config.bulletEndEffect);
        }
        private void SpawnEffect(GameObject effectPrefab)
        {
            Instantiate(effectPrefab, transform.localPosition, transform.localRotation, transform.parent);
        }
    }
}