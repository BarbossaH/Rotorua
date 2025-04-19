namespace Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using MVC;
    using UnityEngine;

    [AutoRegisterManager]
    public class ControllerManager : Singleton<ControllerManager>
    {
        private readonly Dictionary<ControllerType, BaseController> _controllers = new();

        public void RegisterController(ControllerType type, BaseController controller)
        {
            _controllers.TryAdd(type, controller);
        }

        public void UnregisterController(ControllerType type)
        {
            if (_controllers.ContainsKey(type))
            {
                _controllers[type].DestroyController();
                _controllers.Remove(type);
            }
        }


        public void TriggerControllerEvent(ControllerType type, string eventName, params object[] args)
        {
            if (_controllers.TryGetValue(type, out var controller))
            {
                controller.TriggerControllerEvent(eventName, args);
            }
        }
        public void ClearAllController()
        {
            List<ControllerType> keys = _controllers.Keys.ToList();

            for (int i = 0; i < keys.Count; i++)
            {
                //unregister the related events
                _controllers[keys[i]].DestroyController();
                _controllers.Remove(keys[i]);
            }
        }
        private void InitAllControllers()
        {
            foreach (var controller in _controllers)
            {
                controller.Value.InitController();
            }
        }


        public override void Init()
        {
            base.Init();
            //TODO: register controller here, and then initialize them.
            InitAllControllers(); //TODO: initialize all controllers here tomorrow and continue to optimize the manager classes

        }

    }
}