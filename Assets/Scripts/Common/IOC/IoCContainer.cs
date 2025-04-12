using System;
using System.Collections.Generic;

public static class IoCContainer
{
    private static readonly Dictionary<Type, Func<object>> _registrations = new();
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        _registrations[typeof(TInterface)] = () => new TImplementation();
    }

    public static void Register<TInterface>(Func<object> factory)
    {
        _registrations[typeof(TInterface)] = factory;
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
    public static void ClearResolved<T>()
    {
        _services.Remove(typeof(T));
    }

}