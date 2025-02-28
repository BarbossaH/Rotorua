
public class GameManager : Singleton<GameManager>
{
    public static SceneLoaderManager loaderManager;
    public static ControllerManager controllerManager;
    public override void Init()
    {
        //initialize other managers
        loaderManager = new();
        controllerManager = new();
    }
}