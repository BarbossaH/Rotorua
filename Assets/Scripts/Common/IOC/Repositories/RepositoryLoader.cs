namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public static class RepositoryLoader
    {
        private static readonly List<IDataRepository> _repositories = new();

        public static void CollectFromIoC()
        {
            _repositories.Clear();

            foreach (var reg in typeof(IoCContainer).GetField("_singletonInstances", System.Reflection.BindingFlags.NonPublic
             | System.Reflection.BindingFlags.Static).GetValue(null) as Dictionary<Type, object>)
            {
                if (reg.Value is IDataRepository repo)
                {
                    _repositories.Add(repo);
                }
            }

            // 拿到 registrations 的引用
            var registrations = typeof(IoCContainer)
                .GetField("_registrations", System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static)
                .GetValue(null) as Dictionary<Type, Func<object>>;

            // 复制出一份安全的 List（避免在遍历时被修改）
            var regList = registrations.ToList(); // System.Linq

            foreach (var reg in regList)
            {
                var instance = reg.Value.Invoke();
                if (instance is IDataRepository repo)
                {
                    _repositories.Add(repo);
                    Debug.Log(repo.GetType().Name);
                    Debug.Log(reg.Key);
                    IoCContainer.Register(reg.Key, () => instance);
                }
            }
        }
        public static async UniTask LoadAllDataAsync(Action<float> onProgress = null)
        {
            int total = _repositories.Count;
            for (int i = 0; i < total; i++)
            {
                await _repositories[i].LoadDataAsync();
                onProgress?.Invoke((i + 1) / total);
            }
        }

        public static bool IsAllLoaded => _repositories.All(r => r.IsLoaded);
    }
}
