namespace Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine;
    using Cysharp.Threading.Tasks;
    using Common;

    public class ResourceManager : Singleton<ResourceManager>
    {
        private bool _isGameInitialized = false;

        public bool IsGameInitialized => _isGameInitialized;

        private readonly Dictionary<string, AsyncOperationHandle> loadedAssets = new();


        private readonly Dictionary<string, object> loadedLabels = new();

        public async UniTask<T> LoadAssetByAddressAsync<T>(string address, Action<float> onProgress = null) where T : UnityEngine.Object
        {
            if (loadedAssets.TryGetValue(address, out var value))
            {
                return value.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedAssets[address] = handle;
                return handle.Result;
            }
            return null;
        }

        public async UniTask LoadAssetsByAddressAsync(string[] addresses, Action<float> onProgress = null)
        {
            if (addresses == null || addresses.Length == 0) return;

            foreach (var address in addresses)
            {
                await LoadAssetByAddressAsync<UnityEngine.Object>(address, onProgress);
            }
        }
        public async UniTask<IList<T>> LoadAssetsByLabelAsync<T>(string label, Action<float> onProgress = null)
        {
            if (loadedLabels.TryGetValue(label, out var handleList))
            {
                return ((AsyncOperationHandle<IList<T>>)handleList).Result;
            }

            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, (res) =>
            {
                // Debug.Log("loaded a resource" + res.ToString());
            });

            while (!handle.IsDone)
            {
                onProgress?.Invoke(handle.PercentComplete);
                await Task.Yield();
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedLabels[label] = handle;
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load assets with label: {label}. Error: {handle.OperationException}");
                Addressables.Release(handle);
                return null;
            }
        }

        public async UniTask PreloadAssetAsync()
        {
            if (_isGameInitialized) return;
            //����json�ļ�
            await LoadAssetByAddressAsync<AudioClip>("");
        }
        public void UnloadAsset(string address)
        {
            if (loadedAssets.TryGetValue(address, out var value))
            {
                Addressables.Release(value);
                loadedAssets.Remove(address);
            }
            else
            {
                Debug.LogWarning($"Asset at address: {address} is not loaded.");
            }
        }

        public void UnloadAssets(IList<string> addresses)
        {
            foreach (var address in addresses)
            {
                UnloadAsset(address);
            }
        }

    }

}