
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;

// [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
// public class AutoRegisterRepositoryAttribute : Attribute
// {
//     public Type InterfaceType { get; }
//     public AutoRegisterRepositoryAttribute(Type interfaceType)
//     {
//         InterfaceType = interfaceType;
//     }
// }

// public static class IoCContainer
// {
//     private static readonly Dictionary<Type, Func<object>> _registrations = new();
//     private static readonly Dictionary<Type, object> _services = new();

//     public static void Register(Type interfaceType, Func<object> factory)
//     {
//         if (_registrations.ContainsKey(interfaceType))
//         {
//             var existing = _registrations[interfaceType].Method.DeclaringType?.Name ?? "未知类型";
//             throw new InvalidOperationException($"[IoC] 重复注册接口类型：{interfaceType.Name}。已由 {existing} 注册，现在尝试由 {factory.Method.DeclaringType?.Name ?? "未知类型"} 再次注册！");
//         }
//         _registrations[interfaceType] = factory;
//     }

//     public static T GetService<T>()
//     {
//         Type type = typeof(T);
//         if (_services.TryGetValue(type, out var service))
//         {
//             return (T)service;
//         }

//         if (_registrations.TryGetValue(type, out var factory))
//         {
//             var s = factory();
//             _services[type] = s;
//             return (T)s;
//         }
//         throw new InvalidOperationException($"Type {type.Name} is not registered in the container.");
//     }

//     public static T GetTransient<T>()
//     {
//         if (_registrations.TryGetValue(typeof(T), out var factory))
//         {
//             return (T)factory(); // 不缓存，直接 new
//         }
//         throw new InvalidOperationException($"Type {typeof(T).Name} is not registered.");
//     }

//     public static void ClearResolved<T>()
//     {
//         _services.Remove(typeof(T));
//     }

//     public static void AutoRegisterAllRepositories()
//     {
//         var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(
//             assembly =>
//             {
//                 try { return assembly.GetTypes(); }
//                 catch { return Array.Empty<Type>(); }
//             }
//         );

//         foreach (var type in allTypes)
//         {
//             if (!type.IsClass || type.IsAbstract)
//                 continue;

//             var attrs = type.GetCustomAttributes<AutoRegisterRepositoryAttribute>();
//             foreach (var attr in attrs)
//             {
//                 var interfaceType = attr.InterfaceType;

//                 if (_registrations.ContainsKey(interfaceType))
//                 {
//                     var existing = _registrations[interfaceType].Method.DeclaringType?.Name ?? "未知类型";
//                     throw new InvalidOperationException($"[IoC] 重复注册接口类型：{interfaceType.Name}。已由 {existing} 注册，现在尝试由 {type.Name} 再次注册！");
//                 }

//                 Register(interfaceType, () => Activator.CreateInstance(type));
//                 Console.WriteLine($"[IoC] Auto-registered: {interfaceType.Name} => {type.Name}");
//             }
//         }
//     }

//     public static void CollectFromIoCRepositories(List<IDataRepository> _repositories)
//     {
//         _repositories.Clear();

//         foreach (var reg in _services)
//         {
//             if (reg.Value is IDataRepository repo)
//             {
//                 _repositories.Add(repo);
//             }
//         }

//         var registrations = _registrations.ToList();
//         foreach (var reg in registrations)
//         {
//             var instance = reg.Value.Invoke();
//             if (instance is IDataRepository repo)
//             {
//                 _repositories.Add(repo);
//                 Register(reg.Key, () => instance); // 替换为已实例化对象，防止重复加载
//             }
//         }
//     }
// }
