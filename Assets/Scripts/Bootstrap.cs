
using Common;
using Common.Defs;
using Common.Repositories;
using Cysharp.Threading.Tasks;
using Managers;

using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        //start,update game, and then login
        //wondering this could be move to the callback method of the fist loading scene method(SwitchSceneAsync)
        BuildTypeCache();

        //prepare the type of data and factories 
        RegisterServices();
    }


    private async void Start()
    {
        //load persistent scene because it contains all mono services, so prepare it as early as possible
        await SceneLoaderManager.Instance.LoadSceneAsync(SceneName.PersistentScene);
        //load player save
        await LoadGameSaveData();

        GameManager.Instance.Init();

        //loading the next scene which is login, but here, I should can change the scene name so that I can load any scene
        await SceneLoaderManager.Instance.SwitchSceneAsync(SceneName.BootstrapScene, SceneName.TestScene,
            async () =>
            {
                //I think this should store the Type cache here first. and register repository
                await JsonManager.Instance.LoadJsonFile();

                //store the data into corresponding repository
                await RepositoryLoader.LoadAllDataAsync(progress => Debug.Log($"Progress: {progress * 100}%  and ✅ All Repositories Loaded."));
            }
        );

    }
    private void BuildTypeCache()
    {
        GameTypeCache.BuildTypeCache();
    }
    private void RegisterServices()
    {
        //if there is a static data reading from json files, then register it first. So if any methods want to use data, just need to retrieve it from IocContainer, but to make sure the data has been loaded; 
        // IoCContainer.Register<IRepository<SkillData, string>, SkillDataRepository>();
        IoCContainer.AutoRegisterServiceInContainer();
        RepositoryLoader.CollectFromIoC();
    }

    private async UniTask LoadGameSaveData()
    {
        //loading user saving data from json file
        await PersistentDataManager.Instance.LoadAllSaveData();
    }

}
