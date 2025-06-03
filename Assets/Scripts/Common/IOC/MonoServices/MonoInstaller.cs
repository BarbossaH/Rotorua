using UnityEngine;

namespace Common.MonoServices
{
    public class MonoInstaller : MonoBehaviour
    {
        private static bool _initializedInstaller = false;

        private void Awake()
        {
            if (_initializedInstaller)
            {
                Destroy(gameObject);
                return;
            }
            _initializedInstaller = true;
            RegisterAllMonoServices();
        }

        private void RegisterAllMonoServices()
        {
            var monoServices = GetComponentsInChildren<MonoBehaviour>();

            foreach (var service in monoServices)
            {
                if (service is IMonoService)
                {
                    //get all interfaces inherited by the service
                    var interfaceTypes = service.GetType().GetInterfaces();
                    foreach (var iType in interfaceTypes)
                    {
                        //this must be very careful, there are only two interfaces inherited by this service. if one day more interfaces has been inherited by the service, it also needs to exclude those interfaces added.
                        if (iType != typeof(IMonoService))
                        {
                            IoCContainer.RegisterInstance(iType, service);
                            Debug.Log($"Automatically register mono service : {iType.Name}");
                        }
                    }

                }
            }
        }

        private void OnDestroy()
        {
            UnregisterAllMonoServices();
        }

        //To prevent game objects in the persistent scene from being unexpectedly unloaded, mono services need to be removed from 
        // the ioc container, dereferencing them.
        private void UnregisterAllMonoServices()
        {
            var monoServices = GetComponentsInChildren<MonoBehaviour>(true);
            foreach (var service in monoServices)
            {
                if (service is IMonoService)
                {
                    var interfaceTypes = service.GetType().GetInterfaces();
                    foreach (var iType in interfaceTypes)
                    {
                        if (iType != typeof(IMonoService) && iType != typeof(MonoBehaviour))
                        {
                            IoCContainer.UnregisterInstance(iType);
                            Debug.Log($"♻️ automatically unregister MonoService: {iType.Name}");
                        }
                    }
                }
            }
        }
    }


}