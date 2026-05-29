using UnityEngine;

namespace Pool
{
    public abstract class PoolableObject : MonoBehaviour, IPoolable
    {
        public ComponentPool Pool { get; set; }

        public abstract void OnSpawn();
        public abstract void OnDespawn();

        public void ReturnToPool()
        {
            Pool.Return(this);
        }
    }
}