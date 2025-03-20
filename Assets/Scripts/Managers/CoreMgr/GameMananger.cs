
//here is a big problem to solve, because I want GameManager become all managers' manager, but if each manager inherits from Singleton, then they would be independent, which means they are not managed by GameManager
public class GameManager : Singleton<GameManager>
{
    private ResourceManager _resourceManager;
    private SceneLoaderManager _loaderManager;
    private ControllerManager _controllerManager;

    public override void Init()
    {
        //initialize other managers
        _resourceManager = ResourceManager.Instance;
        _loaderManager = SceneLoaderManager.Instance;
        _controllerManager = ControllerManager.Instance;

        //LoadLoginScene();
        RegisterControllers();
    }

    private void RegisterControllers()
    {
        //register controllers
        // _controllerManager.RegisterController(ControllerType.Loading, controller);

        //after the registration of all controllers, then initialize them
        InitAllControllers();
    }

    private void InitAllControllers()
    {
        _controllerManager.InitAllControllers();
    }
    //private async void LoadLoginScene()
    //{
    //   //await resourceManager.LoadSceneAsync("Login");
    //}
}