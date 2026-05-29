using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ComponentPool : MonoBehaviour
    {
        [SerializeField] private PoolableObject prefab;
        [SerializeField] private int initialSize = 20;

        private readonly Queue<PoolableObject> pool = new();

        private void Awake()
        {
            for (int i = 0; i < initialSize; i++)
            {
                CreateObject();
            }
        }

        private PoolableObject CreateObject()
        {
            PoolableObject obj =
                Instantiate(prefab, transform);

            obj.Pool = this;

            obj.gameObject.SetActive(false);

            pool.Enqueue(obj);

            return obj;
        }

        public T Get<T>() where T : PoolableObject
        {
            if (pool.Count == 0)
            {
                CreateObject();
            }

            PoolableObject obj = pool.Dequeue();

            obj.gameObject.SetActive(true);

            obj.OnSpawn();

            return obj as T;
        }

        public void Return(PoolableObject obj)
        {
            obj.OnDespawn();

            obj.gameObject.SetActive(false);

            pool.Enqueue(obj);
        }
    }
}