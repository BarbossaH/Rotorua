namespace Managers
{
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine;
    using UnityEngine.ResourceManagement.ResourceProviders;
    using UnityEngine.SceneManagement;
    using Cysharp.Threading.Tasks;
    using System.Collections.Generic;
    using System;
    using Common;

    public class SceneLoaderManager : Singleton<SceneLoaderManager>
    {
        //current scene
        public readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> loadedScenes = new();

        public event Action<float, string> OnSceneLoading;
        public async UniTask SwitchSceneAsync(string fromScene, string nextScene, Func<UniTask> OnLoadResource = null)
        {
            //send event, ready to unload a scene
            EventCenterManager.Instance.PublishEvent(GameEvents.SceneSwitching);

            //load the loading scene as addictive mode, after this, activate the loading scene
            UpdateLoadingProgress(0.1f);
            await LoadSceneAsync(SceneName.LoadingScene);

            //release unnecessary resources
            UpdateLoadingProgress(0.2f, "Starting");

            if (OnLoadResource != null)
            {
                await OnLoadResource();
                UpdateLoadingProgress(0.7f, "Resources Loaded");
                await UniTask.Delay(5000);
            }
            //unload the current scene
            await UnloadSceneAsync(fromScene);
            UpdateLoadingProgress(1f, "Completed");
            await UniTask.Delay(2000);

            var toSceneHandle = await LoadSceneAsync(nextScene);
            await UniTask.NextFrame();

            if (toSceneHandle.IsValid())
            {
                await UnloadSceneAsync(SceneName.LoadingScene);
            }

            //send an event, finished loading a scene
            EventCenterManager.Instance.PublishEvent(GameEvents.SceneSwitched);

        }

        public async UniTask<AsyncOperationHandle> LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            //if scene exists, then return directly

            if (loadedScenes.TryGetValue(sceneName, out var existingHandle) && existingHandle.IsValid())
            {
                return existingHandle;
            }

            try
            {
                var handle = Addressables.LoadSceneAsync(sceneName, mode);
                await handle.Task;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    loadedScenes[sceneName] = handle;
                    return handle;
                }

                Debug.LogError($"The scene {sceneName} loading failed");

                return default;
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception loading scene {sceneName}: {e.Message}");
                return default;
            }
        }

        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (loadedScenes.TryGetValue(sceneName, out var handle))
            {
                //if handle is valid then remove it from the memory
                if (!handle.IsValid())
                {
                    Debug.LogWarning($"Scene {sceneName} has been unloaded by Unity");
                    loadedScenes.Remove(sceneName);
                    return;
                }
                //ensure handle completely loaded
                await handle.Task;

                try
                {
                    //unload the handle
                    await Addressables.UnloadSceneAsync(handle).Task;
                    //after unloading completely, remove it
                    loadedScenes.Remove(sceneName);
                    Debug.Log($"Scene {sceneName} has been unloaded via Addressables.");
                    return;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to unload scene {sceneName}: {e.Message}");
                    throw;
                }
            }

            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(sceneName);
                Debug.Log($"Scene {sceneName} has been unloaded via SceneManager.");
            }
            else
            {
                Debug.LogWarning($"Scene {sceneName} is not loaded.");
            }
        }

        public bool IsSceneLoaded(string sceneName)
        {
            return loadedScenes.TryGetValue(sceneName, out var handle) && handle.IsValid();
        }

        private void UpdateLoadingProgress(float progress, string des = "")
        {
            OnSceneLoading?.Invoke(progress, des);
        }

    }
}