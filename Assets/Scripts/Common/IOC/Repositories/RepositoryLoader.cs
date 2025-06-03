namespace Common.Repositories
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
            var instancesDict = IoCContainer.GetSingletonInstances();
            foreach (var instance in instancesDict.Values)
            {
                if (instance is IDataRepository repo)
                {
                    _repositories.Add(repo);
                }
            }

            var allRepoTypes = GameTypeCache.GetRepositoryTypeMap().Keys;

            foreach (var type in allRepoTypes)
            {
                if (!instancesDict.ContainsKey(type))
                {
                    if (IoCContainer.GetService(type) is IDataRepository repoInstance)
                    {
                        _repositories.Add(repoInstance);
                        // Debug.Log($"Instantiated repository: {type.Name}");
                    }
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
