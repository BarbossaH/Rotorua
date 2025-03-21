using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class StreamingAssetsManager : Singleton<StreamingAssetsManager>
{
    private const string VERSION_FILE = "version.json";
    private const string SETTINGS_FILE = "settings.json";
    // private const string SAVE_FILE = "save.json";
    // private const string KEYBINDINGS_FILE = "keybindings.json";

    public GameVersion Version { get; private set; }
    // public GameSettings Settings { get; private set; }
    // public GameSave SaveData { get; private set; }
    // public KeyBindings KeyBindings { get; private set; }

    public async UniTask<T> LoadJsonFromStreamingAssets<T>(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        // Debug.Log("path: " + path);

        string json;

        try
        {
            if (path.Contains("://") || path.Contains(":///"))
            {
                using (UnityWebRequest request = UnityWebRequest.Get(path))
                {
                    await request.SendWebRequest();
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Failed to load {fileName}: {request.error}");
                        return default;
                    }
                    json = request.downloadHandler.text;
                }
            }
            else
            {
                json = await File.ReadAllTextAsync(path);
            }

            // Check if JSON is empty
            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogError($"JSON file {fileName} is empty or invalid.");
                return default;
            }

            return JsonUtility.FromJson<T>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception while loading {fileName}: {ex.Message}");
            return default;
        }
    }

}