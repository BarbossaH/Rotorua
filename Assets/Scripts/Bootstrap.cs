using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        //start,update game, and then login���������ڵ�������ǿ�����������Ϸ��ʱ����һЩ���µ�����

        GameManager.Instance.Init();

    }

    private async void Start()
    {

        //loading the next scene which is login, but here, I should can change the scene name so that I can load any scene

        await GameManager.loaderManager.SwitchSceneAsync(SceneName.BootstrapScene, SceneName.LoginScene);
    }
}
