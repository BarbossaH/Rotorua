using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using Unity.VisualScripting;

public class ResourceManager : Singleton<ResourceManager>
{
    // 单个资源缓存 (按地址存储)
    private readonly Dictionary<string, AsyncOperationHandle> loadedAssets = new();

    // 多个资源缓存 (按 Label 存储)
    private readonly Dictionary<string, object> loadedLabels = new();

    public async Task<T> LoadAssetAsync<T>(string address, Action<float> onProgress = null) where T : UnityEngine.Object
    {
        if (loadedAssets.TryGetValue(address, out var value))
        {
            return value.Result as T;
        }

        var handle = Addressables.LoadAssetAsync<T>(address);
        while (!handle.IsDone)
        {
            onProgress?.Invoke(handle.PercentComplete);
            await Task.Yield();
        }

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            loadedAssets[address] = handle;
            return handle.Result;
        }
        return null;
    }

    public async Task<IList<T>> LoadAssetsAsync<T>(string label, Action<float> onProgress = null)
    {
        if (loadedLabels.TryGetValue(label, out var handleList))
        {
            return ((AsyncOperationHandle<IList<T>>)handleList).Result;
        }

        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, (res) =>
        {
            Debug.Log("加载了一个资源：" + res.ToString());
        });

        while (!handle.IsDone)
        {
            onProgress?.Invoke(handle.PercentComplete);
            await Task.Yield();
        }

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            //loadedLabels[label] = handle; // 存入 object 避免泛型转换问题
            //return handle.Result;
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
