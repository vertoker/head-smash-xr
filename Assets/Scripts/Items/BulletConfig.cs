using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = nameof(BulletConfig), menuName = "Items/" + nameof(BulletConfig))]
    public class BulletConfig : ScriptableObject
    {
        [field:SerializeField] public GameObject bulletStartEffect { get; private set; }
        [field:SerializeField] public GameObject bulletEndEffect { get; private set; }
    }
}