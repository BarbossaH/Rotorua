
namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public enum TypeCategoryEnum
    {
        StartUp, //when the game start up, the types will be created like data repository for storing data
        Lazy,
        EditorOnly
    }
    public static class GameTypeCache
    {
        // private static readonly Dictionary<TypeCategoryEnum, List<Type>> _typeGroups = new();
        private static readonly List<Type> _startUpTypes = new();
        private static readonly Dictionary<string, Type> _delayLoadTypes = new();

        // static GameTypeCache()
        // {
        //     foreach (TypeCategoryEnum c in Enum.GetValues(typeof(TargetTypeEnum)))
        //     {
        //         _typeGroups[c] = new List<Type>();
        //     }
        // }

        public static void BuildTypeCache(Predicate<Type> filer = null)
        {
            //get all types in the game

            IEnumerable<Type> allTypes;
            try { allTypes = Assembly.GetExecutingAssembly().GetTypes(); }
            catch { allTypes = Array.Empty<Type>(); }


            //filtering the types, the condition is not class, abstract class, not to meet the conditions of the filter method, then neglect them.
            foreach (var type in allTypes)
            {
                if (!type.IsClass || type.IsAbstract) continue;
                if (filer != null && !filer(type)) continue;

                //get the attribute of the type by the given type(TypeCategoryAttribute)
                var attr = type.GetCustomAttribute<TypeCategoryAttribute>();
                if (attr == null) continue;
                switch (attr.Category)
                {
                    case TypeCategoryEnum.StartUp:
                        _startUpTypes.Add(type);
                        break;

                    case TypeCategoryEnum.Lazy:
                        // 注意这里用 type.Name，不包含 namespace，因为你的 json 里就存类名
                        if (!_delayLoadTypes.ContainsKey(type.Name))
                            _delayLoadTypes[type.Name] = type;
                        break;
                }
            }
            // Console.WriteLine("[TypeCache] Build complete. Type counts:");
            foreach (var t in _startUpTypes)
            {
                Debug.Log(t.Name + ": is the " + t.FullName);
                // Console.WriteLine($"- {kvp.Key}: {kvp.Value.Count} types");
            }
        }

        public static object CreateLazyInstance(string typeName)
        {
            Type type = GetLazyType(typeName);
            if (type != null)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static T CreateLazyInstance<T>(string typeName) where T : class
        {
            return CreateLazyInstance(typeName) as T;
        }


        public static Type GetLazyType(string className)
        {
            if (_delayLoadTypes.TryGetValue(className, out var type))
            {
                return type;
            }
            return null;
        }

        public static List<Type> GetStartUpTypes()
        {
            return _startUpTypes;
        }
    }
}
