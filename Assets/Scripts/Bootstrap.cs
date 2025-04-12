
using System.Threading.Tasks;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        //start,update game, and then login
        GameManager.Instance.PreInit();
        RegisterRepository();
    }

    private void RegisterRepository()
    {
        IoCContainer.Register<IRepository<SkillData, string>, SkillDataRepository>();

    }
    private async void Start()
    {

        GameManager.Instance.Init();

        GameVersion version = await StreamingAssetsManager.Instance.LoadJsonFromStreamingAssets<GameVersion>("version.json");

        //loading user saving data
        PlayerData data = await PersistentDataManager.Instance.LoadData<PlayerData>("player_save.json");
        // Debug.Log(data.playerName);
        //loading the next scene which is login, but here, I should can change the scene name so that I can load any scene

        await SceneLoaderManager.Instance.SwitchSceneAsync(SceneName.BootstrapScene, SceneName.TestScene,
            async () =>
            {
                await JsonManager.Instance.LoadJsonFile();
                var skillResp = IoCContainer.GetService<IRepository<SkillData, string>>();
                await skillResp.LoadDataAsync();
            }
        );

    }


}
