
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static ResourceManager resourceManager;
    public static SceneLoaderManager loaderManager;
    public static ControllerManager controllerManager;
    
    public override async void Init()
    {
        //initialize other managers
        resourceManager = new();
        loaderManager = new();
        controllerManager = new();

        //await resourceManager.LoadAssetAsync<GameObject>("test2", (per) => { Debug.Log(123); });
        await resourceManager.LoadAssetsAsync<GameObject>("testPre", (pre) => { Debug.Log(pre); });
    }
}