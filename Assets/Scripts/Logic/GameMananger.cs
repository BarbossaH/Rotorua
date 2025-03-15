

using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public static ResourceManager resourceManager;
    public static SceneLoaderManager loaderManager;
    public static ControllerManager controllerManager;
    
    public override void Init()
    {
        //initialize other managers
        resourceManager = new();
        loaderManager = new();
        controllerManager = new();

        //LoadLoginScene();
        RegisterControllers();
    }

    private void RegisterControllers()
    {
        //register controllers

        //after the registration of all controllers, then initialize them
        InitAllControllers();
    }

    private void InitAllControllers() 
    {
        GameManager.controllerManager.InitAllControllers();
    }
    //private async void LoadLoginScene()
    //{
    //   //await resourceManager.LoadSceneAsync("Login");
    //}
}