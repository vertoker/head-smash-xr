using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = nameof(GunConfig), menuName = "Items/" + nameof(GunConfig))]
    public class GunConfig : ScriptableObject
    {
        [field:SerializeField] public float rate { get; private set; } = 0.2f;
        [field:SerializeField] public float bulletSpeed { get; private set; } = 20f;
        [field:SerializeField] public GameObject bulletPrefab { get; private set; }
    }
}