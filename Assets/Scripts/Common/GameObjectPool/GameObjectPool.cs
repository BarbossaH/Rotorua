using System.Collections.Generic;
using UnityEngine;

namespace Common.ObjectPool
{
    public interface IObjectPool
    {
        GameObject RentObj();
        void ReturnObj(GameObject obj);
    }

    #region Generic game object pool
    /// <summary>
    /// this class is for returning components of a game object, not a game object.Additionally, if a prefab contains multiple components—for example, both particle and audio source components—this object pool can also be used to create it.
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    public class ObjectPool<T> : IObjectPool, IDisposable where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        private readonly Stack<T> _pool = new();

        public int PoolCount => _pool.Count;
        public ObjectPool(T prefab, int initCount = 5, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < initCount; i++)
            {
                var instance = CreateNew();
                instance.gameObject.SetActive(false);
                _pool.Push(instance);
            }

        }
        public T RentObj()
        {
            var obj = _pool.Count > 0 ? _pool.Pop() : CreateNew();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObj(T obj)
        {
            if (_pool.Contains(obj))
            {
                Debug.LogWarning($"[GameObjectPool] Tried to return duplicate object: {obj.name}");
                return;
            }
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }

        private T CreateNew()
        {
            var instance = GameObject.Instantiate(_prefab, _parent);
            instance.gameObject.name += " (Pooled)";
            return instance;
        }

        //this is the implicit implement of the interface
        GameObject IObjectPool.RentObj()
        {
            return RentObj().gameObject;
        }

        //this is the implicit implement of the interface
        void IObjectPool.ReturnObj(GameObject obj)
        {
            var component = obj.GetComponent<T>();
            if (component != null)
            {
                ReturnObj(component);
            }
            else
            {
                Debug.LogWarning($"[ObjectPool<{typeof(T).Name}>] GameObject does not contain component {typeof(T).Name}");
                GameObject.Destroy(obj);
            }
        }

        public void Dispose()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                GameObject.Destroy(obj.gameObject);
            }
        }
    }

    #endregion


    #region GameObject Pool for returning a Game Object
    public class GameObjectPool : IObjectPool, IDisposable
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly Stack<GameObject> _pool = new();
        public int PoolCount => _pool.Count;

        public GameObjectPool(GameObject prefab, int initCount = 5, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < initCount; i++)
            {
                var obj = CreateNew();
                obj.SetActive(false);
                _pool.Push(obj);
            }
        }
        public GameObject RentObj()
        {
            var obj = _pool.Count > 0 ? _pool.Pop() : CreateNew();
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObj(GameObject obj)
        {
            obj.SetActive(false);
            _pool.Push(obj);
        }

        private GameObject CreateNew()
        {
            return GameObject.Instantiate(_prefab, _parent);
        }

        public void Dispose()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                GameObject.Destroy(obj);
            }
        }
    }
    #endregion
}