
using System.Threading.Tasks;
using UnityEngine;
public class Bootstrap : MonoBehaviour
{

    private void Awake()
    {
        //start,update game, and then login
        GameManager.Instance.Init();
    }


    private async void Start()
    {
        //loading game setting
        //   await  PersistentDataManager.Instance.SaveData<PlayerData>(new PlayerData(){
        //     playerName="Test"
        //   },"player_save.json");

        await JsonManager.Instance.LoadAllJsonByLabelAsync("json_data");

        TestData testData = JsonManager.Instance.GetData<TestData>("TestData");

        // Debug.Log(testData.var1);

        GameVersion version = await StreamingAssetsManager.Instance.LoadJsonFromStreamingAssets<GameVersion>("version.json");
        // Debug.Log(version.changelog[0]);
        // Debug.Log(version.changelog[1]);
        //loading user saving data
        PlayerData data = await PersistentDataManager.Instance.LoadData<PlayerData>("player_save.json");
        // Debug.Log(data.playerName);
        //loading the next scene which is login, but here, I should can change the scene name so that I can load any scene

        await SceneLoaderManager.Instance.SwitchSceneAsync(SceneName.BootstrapScene, SceneName.LoginScene);

    }


}
