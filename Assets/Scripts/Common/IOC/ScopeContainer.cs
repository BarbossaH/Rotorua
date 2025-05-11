using System;
using System.Collections.Generic;

namespace Common
{
    public class ScopeContainer : IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _registrations = new();
        private readonly Dictionary<Type, object> _scopeInstances = new();

        private bool _isDisposed;
        public ScopeContainer(Dictionary<Type, Func<object>> registrations)
        {
            _registrations = registrations;
        }

        public T GetService<T>()
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ScopeContainer));

            var type = typeof(T);

            if (_scopeInstances.TryGetValue(type, out var instance))
            {
                return (T)instance;
            }
            if (_registrations.TryGetValue(type, out var factory))
            {
                var s = factory();
                _scopeInstances[type] = s;
                return (T)s;
            }
            throw new InvalidOperationException($"Type {type.Name} is not registered in the container.");
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;

            foreach (var instance in _scopeInstances.Values)
            {
                if (instance is IDisposable d)
                {
                    d.Dispose();
                }
            }
            _scopeInstances.Clear();
        }
    }
}