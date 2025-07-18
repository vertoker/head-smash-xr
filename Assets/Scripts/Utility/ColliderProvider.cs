using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utility
{
    /// <summary>
    /// Простой и расширяемый utility скрипт, который позволяет скриптам извне использовать данные о том,
    /// как объект взаимодействует с коллайдерами. Имеет опциональные фильтры на
    /// тип взаимодействия, mask или tag объекта взаимодействия
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ColliderProvider : MonoBehaviour
    {
        [field:SerializeField] public bool provideColliders { get; private set; } = true;
        [field:SerializeField] public bool provideTriggers { get; private set; } = false;
        
        [field:Space]
        [field:SerializeField] public bool filterByLayer { get; private set; } = false;
        [field:SerializeField] public LayerMask maskFilter { get; private set; } = unchecked((int)0xFFFFFFFF); // -1
        
        [field:SerializeField] public bool filterByTag { get; private set; } = false;
        [field:SerializeField] public string tagFilter { get; private set; } = string.Empty;
        
        [field:Space]
        [field:SerializeField] public bool debug { get; private set; } = false;
        
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerStay;
        public event Action<Collider> TriggerExit;
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionStay;
        public event Action<Collision> CollisionExit;

        private bool FilterEvents(Collider other)
        {
            if (!provideTriggers && other.isTrigger) return true;
            if (!provideColliders && !other.isTrigger) return true;
            
            var obj = other.gameObject;
            if (filterByLayer && !Contains(maskFilter, obj.layer)) return true;
            if (filterByTag && obj.CompareTag(tagFilter)) return true;
            
            return false;
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (FilterEvents(other)) return;
            TriggerEnter?.Invoke(other);
            if (debug)
                Debug.Log($"{nameof(OnTriggerEnter)}, {other.name}", other.gameObject);
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            if (FilterEvents(other)) return;
            TriggerStay?.Invoke(other);
            if (debug)
                Debug.Log($"{nameof(OnTriggerStay)}, {other.name}", other.gameObject);
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (FilterEvents(other)) return;
            TriggerExit?.Invoke(other);
            if (debug)
                Debug.Log($"{nameof(OnTriggerExit)}, {other.name}", other.gameObject);
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (FilterEvents(other.collider)) return;
            CollisionEnter?.Invoke(other);
            if (debug)
                Debug.Log($"{nameof(OnCollisionEnter)}, {other.gameObject.name}", other.gameObject);
        }
        protected virtual void OnCollisionStay(Collision other)
        {
            if (FilterEvents(other.collider)) return;
            CollisionStay?.Invoke(other);
            if (debug)
                Debug.Log($"{nameof(OnCollisionStay)}, {other.gameObject.name}", other.gameObject);
        }
        protected virtual void OnCollisionExit(Collision other)
        {
            if (FilterEvents(other.collider)) return;
            CollisionExit?.Invoke(other);
            if (debug)
                Debug.Log($"{nameof(OnCollisionExit)}, {other.gameObject.name}", other.gameObject);
        }

        public void Set(ColliderProvider provider)
        {
            filterByLayer = provider.filterByLayer;
            maskFilter = provider.maskFilter;
            filterByTag = provider.filterByTag;
            tagFilter = provider.tagFilter;
            debug = provider.debug;
        }
        
        private static bool Contains(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}