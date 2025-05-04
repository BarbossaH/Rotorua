
using Common;
using Managers;
using Models;
using Unity.VisualScripting;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        //start,update game, and then login
        //wondering this could be move to the callback method of the fist loading scene method(SwitchSceneAsync)
        BuildTypeCache();
    }


    private async void Start()
    {
        //prepare the data and factories
        RegisterServices();

        GameManager.Instance.Init();

        GameVersion version = await StreamingAssetsManager.Instance.LoadJsonFromStreamingAssets<GameVersion>("version.json");

        //loading user saving data
        // GameSettings data = await PersistentDataManager.Instance.LoadData<GameSettings>("game_setting.json");
        GameDataSave data = await PersistentDataManager.Instance.LoadData<GameDataSave>("game_data.json");
        if (data != null && data.SaveID != null && data.HeroSaveData != null && data.RelicSaveData != null)
        {
            Debug.Log(data?.SaveID + " and its language is " + ((HeroSaveData)data?.HeroSaveData)?.CurrentHP + "and relic id is" + ((RelicSaveData)data?.RelicSaveData[0])?.ID);

        }
        //loading the next scene which is login, but here, I should can change the scene name so that I can load any scene

        await SceneLoaderManager.Instance.SwitchSceneAsync(SceneName.BootstrapScene, SceneName.TestScene,
            async () =>
            {
                //I think this should store the Type cache here first. and register repository
                await JsonManager.Instance.LoadJsonFile();
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

}
