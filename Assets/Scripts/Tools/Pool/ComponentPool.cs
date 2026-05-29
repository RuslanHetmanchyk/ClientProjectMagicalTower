using System.Collections.Generic;
using UnityEngine;

namespace Tools.Pool
{
    public class ComponentPool : MonoBehaviour
    {
        [SerializeField] private PoolableObject prefab;
        [SerializeField] private int initialSize = 20;

        private readonly Queue<PoolableObject> pool = new();

        private void Awake()
        {
            for (var i = 0; i < initialSize; i++)
            {
                CreateObject();
            }
        }

        private PoolableObject CreateObject()
        {
            var poolableObject = Instantiate(prefab, transform);
            poolableObject.Pool = this;
            poolableObject.gameObject.SetActive(false);

            pool.Enqueue(poolableObject);

            return poolableObject;
        }

        public T Get<T>() where T : PoolableObject
        {
            if (pool.Count == 0)
            {
                CreateObject();
            }

            var poolableObject = pool.Dequeue();
            poolableObject.gameObject.SetActive(true);
            poolableObject.OnSpawn();

            return poolableObject as T;
        }

        public void Return(PoolableObject obj)
        {
            obj.OnDespawn();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}