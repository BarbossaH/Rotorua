
namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public static class GameTypeCache
    {
        #region fields
        private static readonly Dictionary<string, Type> _typeByName = new();
        private static readonly Dictionary<string, Type> _typeByFullName = new();
        private static readonly Dictionary<Type, List<Type>> _subclassCache = new();
        private static readonly List<Type> _allCachedTypes = new();

        //these types are for the repositories which need to be created when start the game
        private static readonly List<Type> _repositoryList = new();
        private static readonly Dictionary<Type, Type> _repositoryDict = new();
        private static readonly Dictionary<Type, Type> _factoryDict = new();
        private static readonly Dictionary<Type, Type> _instanceIdDict = new();
        private static readonly Dictionary<Type, Type> _controllerDict = new();
        private static readonly List<Type> _managerList = new();
        #endregion
        //this should be ready before using it, so it should be in the awake method of bootstrap script
        public static void BuildTypeCache(Predicate<Type> globalFilter = null)
        {
            ClearAll();

            //get all types in the game
            IEnumerable<Type> allTypes;
            try { allTypes = Assembly.GetExecutingAssembly().GetTypes(); }
            catch { allTypes = Array.Empty<Type>(); }

            //filtering the types, the condition is not class, abstract class, not to meet the conditions of the filter method, then neglect them.
            foreach (var type in allTypes)
            {
                if (!type.IsClass || type.IsAbstract) continue;
                if (globalFilter != null && !globalFilter(type)) continue;

                _typeByName[type.Name] = type;
                _typeByFullName[type.FullName] = type;
                _allCachedTypes.Add(type);
                var attr = type.GetCustomAttribute<AutoRegisterRepositoryAttribute>();
                if (attr != null)
                {
                    _repositoryList.Add(type);
                    _repositoryDict[attr.ClassNameType] = type;
                    // Debug.Log(type.Name);
                }

                var factoryAttr = type.GetCustomAttribute<AutoRegisterFactoryAttribute>();
                if (factoryAttr != null)
                {
                    _factoryDict[factoryAttr.InterfaceType] = type;
                    // Debug.Log(type.Name);
                }

                var managerAttr = type.GetCustomAttribute<AutoRegisterManagerAttribute>();
                if (managerAttr != null)
                {
                    _managerList.Add(type);
                    // Debug.Log(type.Name);
                }

                var idAttr = type.GetCustomAttribute<AutoRegisterIDGeneratorAttribute>();
                if (idAttr != null)
                {
                    _instanceIdDict[idAttr.InterfaceType] = type;
                }

                var controllerAttr = type.GetCustomAttribute<AutoRegisterControllerAttribute>();
                if (controllerAttr != null)
                {
                    _controllerDict[controllerAttr.InterfaceType] = type;
                }

            }
#if UNITY_EDITOR
            // foreach (var t in _repositoryList)
            // {
            //     Debug.Log(t.Name + ": is the " + t.FullName);
            // }

            // foreach (var t in _factoryMap)
            // {
            //     Debug.Log(t.Key.Name + ": is the " + t.Value.Name + " and the full name is " + t.Value.FullName);
            // }
#endif
        }

        private static void ClearAll()
        {
            _typeByName.Clear();
            _typeByFullName.Clear();
            _subclassCache.Clear();
            _allCachedTypes.Clear();
            _repositoryList.Clear();
            _repositoryDict.Clear();
            _factoryDict.Clear();
            _managerList.Clear();
            _controllerDict.Clear();
        }

        public static Type GetTypeByName(string typeName)
        {
            return _typeByName.TryGetValue(typeName, out var t) ? t : null;
        }

        public static Type GetTypeByFullName(string fullName)
        {
            return _typeByFullName.TryGetValue(fullName, out var t) ? t : null;
        }

        public static object CreateInstanceByClassName(Type baseType, string name, bool throwError = false)
        {
            var type = GetTypeByName(name);

            if (type == null)
            {
                if (throwError)
                {
                    throw new Exception($"[TypeCache] Type '{name}' not found.");
                }
                return null;
            }

            if (!baseType.IsAssignableFrom(type))
            {
                if (throwError)
                {
                    throw new Exception($"[TypeCache] Type '{name}' is not assignable to {baseType.Name}.");
                }
                return null;
            }

            var instance = Activator.CreateInstance(type);
            if (instance == null && throwError)
            {
                throw new Exception($"[TypeCache] Failed to create instance of '{name}'.");
            }
            // Debug.Log(instance + " is the name of " + type);
            return instance;
        }

        public static T CreateInstanceByClassName<T>(string name, bool throwError = false) where T : class
        {
            return CreateInstanceByClassName(typeof(T), name, throwError) as T;
        }

        public static List<Type> GetTypesOf(Type baseType, Func<Type, bool> filter = null)
        {
            //if this type already be cached in _subclassCache, then return the corresponding list directly
            if (_subclassCache.TryGetValue(baseType, out var cached))
            {
                if (filter == null)
                {
                    return cached;
                }
                return cached.Where(t => filter(t)).ToList();
            }

            //if the type is not in _subclassCache, then looking for it in the all type cache. After finding it, store it in _subclassCache
            var result = _allCachedTypes
            .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
            .Where(t => filter == null || filter(t))
            .ToList();
            _subclassCache[baseType] = result;
            return result;
        }

        public static List<Type> GetTypesOf<T>(Func<Type, bool> filter = null)
        {
            return GetTypesOf(typeof(T), filter);
        }
        public static Dictionary<Type, Type> GetRepositoryTypeMap() => _repositoryDict;

        public static List<Type> GetManagerTypeList() => _managerList;

        public static Dictionary<Type, Type> GetFactoryTypeMap() => _factoryDict;

        public static Dictionary<Type, Type> GetIDGeneratorMap() => _instanceIdDict;

        public static Dictionary<Type, Type> GetControllerMap() => _controllerDict;
    }
}
