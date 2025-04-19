namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public static class IoCContainer
    {
        private static readonly Dictionary<Type, Func<object>> _registrations = new();
        private static readonly Dictionary<Type, object> _services = new();

        // public static void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
        // {
        //     _registrations[typeof(TInterface)] = () => new TImplementation();
        // }

        // public static void Register<TInterface>(Func<object> factory)
        // {
        //     _registrations[typeof(TInterface)] = factory;
        // }

        public static void Register(Type interfaceType, Func<object> factory)
        {
            _registrations[interfaceType] = factory;
        }

        public static T GetService<T>()
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            if (_registrations.TryGetValue(type, out var factory))
            {
                var s = factory();
                _services[type] = s;
                return (T)s;
            }
            throw new InvalidOperationException($"Type {type.Name} is not registered in the container.");
        }

        //for the time being, I don't need the below function, I just write down as study
        public static T GetTransient<T>()
        {
            if (_registrations.TryGetValue(typeof(T), out var factory))
            {
                return (T)factory(); // 不缓存，直接 new
            }
            throw new InvalidOperationException($"Type {typeof(T).Name} is not registered.");
        }

        public static void ClearResolved<T>()
        {
            _services.Remove(typeof(T));
        }

        public static void AutoRegisterAllRepositories()
        {

            var respMap = GameTypeCache.GetRepositoryTypeMap();

            foreach (var pair in respMap)
            {
                Register(pair.Key, () => Activator.CreateInstance(pair.Value));
                Debug.Log($"{pair.Key.Name} has been created instance and value is {pair.Value.Name}");
                // foreach (var attr in attrs)
                // {
                //     Register(interfaceType, () => Activator.CreateInstance(type));
                //     Console.WriteLine($"[IoC] Auto-registered: {interfaceType.Name} => {type.Name}");
                // }
            }
        }
    }
}