
// //here is a big problem to solve, because I want GameManager become all managers' manager, but if each manager inherits from Singleton, then they would be independent, which means they are not managed by GameManager
// public class GameManager : Singleton<GameManager>
// {
//     private ResourceManager _resourceManager;
//     private SceneLoaderManager _loaderManager;
//     private ControllerManager _controllerManager;

//     public override void Init()
//     {
//         //initialize other managers
//         _resourceManager = ResourceManager.Instance;
//         _loaderManager = SceneLoaderManager.Instance;
//         _controllerManager = ControllerManager.Instance;

//         //LoadLoginScene();
//         RegisterControllers();
//     }

//     private void RegisterControllers()
//     {
//         //register controllers
//         // _controllerManager.RegisterController(ControllerType.Loading, controller);

//         //after the registration of all controllers, then initialize them
//         InitAllControllers();
//     }

//     private void InitAllControllers()
//     {
//         _controllerManager.InitAllControllers();
//     }
//     //private async void LoadLoginScene()
//     //{
//     //   //await resourceManager.LoadSceneAsync("Login");
//     //}
// }

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<IGameManger> _managers = new();

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
        var managerTypes = typeof(IGameManger).Assembly.GetTypes()
            .Where(t => typeof(IGameManger).IsAssignableFrom(t)
                        && t != typeof(GameManager)  //must exclude game manger, otherwise it occurs a dead loop
                        && !t.IsInterface
                        && !t.IsAbstract
                        && t.BaseType != null
                        && t.BaseType.IsGenericType
                        && t.BaseType.GetGenericTypeDefinition() == typeof(Singleton<>)); // 确保是 Singleton<T> 的子类

        Debug.Log($"Found {managerTypes.Count()} potential manager types");

        foreach (var type in managerTypes)
        {
            // Debug.Log($"Checking type: {type.FullName}");

            // 直接访问 Singleton<T>.Instance
            var instanceProp = type.BaseType.GetProperty("Instance",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            if (instanceProp != null)
            {
                var instance = instanceProp.GetValue(null) as IGameManger;
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