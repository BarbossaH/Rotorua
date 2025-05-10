namespace Common
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class IoCContainer
    {
        private static readonly Dictionary<Type, Func<object>> _registrations = new();
        private static readonly Dictionary<Type, object> _singletonInstances = new();

        public static void Register(Type interfaceType, Func<object> factory)
        {
            _registrations[interfaceType] = factory;
        }

        public static T GetService<T>()
        {
            Type type = typeof(T);
            if (_singletonInstances.TryGetValue(type, out var instance))
            {
                return (T)instance;
            }

            if (_registrations.TryGetValue(type, out var factory))
            {
                var s = factory();
                _singletonInstances[type] = s;
                return (T)s;
            }
            throw new InvalidOperationException($"Type {type.Name} is not registered in the container.");
        }

        // //for the time being, I don't need the below function, I just write down as study
        // public static T GetTransient<T>()
        // {
        //     if (_registrations.TryGetValue(typeof(T), out var factory))
        //     {
        //         return (T)factory(); // 不缓存，直接 new
        //     }
        //     throw new InvalidOperationException($"Type {typeof(T).Name} is not registered.");
        // }

        public static void ClearResolved<T>()
        {
            _singletonInstances.Remove(typeof(T));
        }

        public static void AutoRegisterServiceInContainer()
        {
            AutoRegisterAllFactories();
            AutoRegisterAllRepositories();
            AutoRegisterAllIDGenerator();
        }
        private static void AutoRegisterAllFactories()
        {
            var facMap = GameTypeCache.GetFactoryTypeMap();
            foreach (var pair in facMap)
            {
                Register(pair.Key, () => Activator.CreateInstance(pair.Value));
            }
        }
        private static void AutoRegisterAllRepositories()
        {
            var respMap = GameTypeCache.GetRepositoryTypeMap();

            foreach (var pair in respMap)
            {
                Register(pair.Key, () => Activator.CreateInstance(pair.Value));
                // Debug.Log($"{pair.Key.Name} has been created instance and value is {pair.Value.Name}");
            }
        }

        private static void AutoRegisterAllIDGenerator()
        {
            var idMap = GameTypeCache.GetIDGeneratorMap();
            foreach (var pair in idMap)
            {
                Register(pair.Key, () => Activator.CreateInstance(pair.Value));
            }
        }
    }
}