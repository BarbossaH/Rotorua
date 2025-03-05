using UnityEngine;
using UnityEngine.SceneManagement;
public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        //start,update game, and then login。这个类存在的意义就是可以在启动游戏的时候做一些更新的事情

        GameManager.Instance.Init();

        //here I can jump to the login scene directly because it's just starter scene
        //SceneManager.LoadScene(Scename.LoginScene);
    }
}
