using System.Collections.Generic;
using Common;
using Common.ObjectPool;
using UnityEngine;

namespace Managers
{
    [AutoRegisterManager]
    public class GamePoolManager : Singleton<GamePoolManager>
    {
        private readonly Dictionary<string, IObjectPool> _poolDict = new();

        public void RegisterGameObjectPool(string key, GameObject prefab, int initCount = 5)
        {
            if (_poolDict.ContainsKey(key)) return;
            _poolDict[key] = new GameObjectPool(prefab, initCount);
        }

        public void RegisterComponentPool<T>(string key, T prefab, int initCount = 5) where T : Component
        {
            if (_poolDict.ContainsKey(key)) return;
            _poolDict[key] = new ObjectPool<T>(prefab, initCount);
        }

        public GameObject RentObj(string key)
        {
            if (_poolDict.TryGetValue(key, out var pool))
            {
                return pool.RentObj();
            }
            return null;
        }

        public void ReturnObj(string key, GameObject obj)
        {
            if (_poolDict.TryGetValue(key, out var pool))
            {
                pool.ReturnObj(obj);
            }
            else
            {
                GameObject.Destroy(obj);
            }
        }

        public T RentObj<T>(string key) where T : Component
        {
            if (_poolDict.TryGetValue(key, out var pool))
            {
                if (pool is ObjectPool<T> componentPool)
                {
                    return componentPool.RentObj();
                }
            }

            return null;
        }

        public void ReturnObj<T>(string key, T component) where T : Component
        {
            if (_poolDict.TryGetValue(key, out var pool))
            {
                if (pool is ObjectPool<T> componentPool)
                {
                    componentPool.ReturnObj(component);
                }
            }

            GameObject.Destroy(component.gameObject);
        }

        public void ClearAllPools()
        {
            foreach (var pool in _poolDict.Values)
            {
                if (pool is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _poolDict.Clear();
        }
    }
}