namespace Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {
        private List<ISingleton> _managers = new();


        public override void Init()
        {
            base.Init();
            RegisterAllManagers();
            InitAllManagers();
        }

        public void ShutDown()
        {
            ReleaseAllManagers();
        }

        private void RegisterAllManagers()
        {
            var mgrTypes = GameTypeCache.GetManagerTypeList();

            foreach (var type in mgrTypes)
            {
                // Debug.Log($"Checking type: {type.FullName}");

                var instanceProp = type.BaseType.GetProperty("Instance",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

                if (instanceProp != null)
                {
                    if (instanceProp.GetValue(null) is ISingleton instance)
                    {
                        _managers.Add(instance);
                        // Debug.Log(_managers.Count);
                        // Debug.Log($"Successfully registered manager: {type.Name}");
                    }
                    else
                    {
                        Debug.LogWarning($"Instance property returned null for {type.Name}");
                    }
                }
                else
                {
                    Debug.LogWarning($"No Instance property found for {type.Name}");
                }
            }
        }


        private void InitAllManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Init();
            }
        }

        private void ReleaseAllManagers()
        {
            for (int i = _managers.Count - 1; i >= 0; i--)
            {
                _managers[i].Release();
            }
        }


    }
}