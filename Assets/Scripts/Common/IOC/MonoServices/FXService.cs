using UnityEngine;

namespace Common.MonoServices
{
    //this interface must exist, or it cannot be registered into ioc container
    public interface IFXService
    {
        void TestPlay();
    }
    public class FXService : MonoBehaviour, IMonoService, IFXService
    {

        public void TestPlay()
        {
            Debug.Log("FX is working");
        }
    }
}