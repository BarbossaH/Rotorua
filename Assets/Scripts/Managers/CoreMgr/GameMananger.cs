namespace Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {
        private List<ISingleton> _managers = new();

        public override void PreInit()
        {
            RegisterAllManagers();
        }
        public override void Init()
        {
            base.Init();
            InitAllManagers();
        }

        public void ShutDown()
        {
            ReleaseAllManagers();
        }
        private void RegisterAllManagers()
        {
            var managerTypes = typeof(ISingleton).Assembly.GetTypes()
                .Where(t => typeof(ISingleton).IsAssignableFrom(t)
                            && t != typeof(GameManager)  //must exclude game manger, otherwise it occurs a dead loop
                            && !t.IsInterface
                            && !t.IsAbstract
                            && t.BaseType != null
                            && t.BaseType.IsGenericType
                            && t.BaseType.GetGenericTypeDefinition() == typeof(Singleton<>)); // ensure it is the child of class Singleton<T>

            Debug.Log($"Found {managerTypes.Count()} potential manager types");

            foreach (var type in managerTypes)
            {
                // Debug.Log($"Checking type: {type.FullName}");

                var instanceProp = type.BaseType.GetProperty("Instance",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

                if (instanceProp != null)
                {
                    var instance = instanceProp.GetValue(null) as ISingleton;
                    if (instance != null)
                    {
                        _managers.Add(instance);
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