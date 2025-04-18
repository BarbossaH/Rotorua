namespace Models
{
    [System.Serializable]
    public class GameVersion
    {
        public string version = "1.0.0";
        public string releaseDate = "2025-03-12";
        public string[] changelog;
    }

    // [System.Serializable]
    // public class GameSettings
    // {
    //     public float volume = 0.8f;
    //     public int resolutionIndex = 2;
    //     public bool fullscreen = true;
    // }

    // [System.Serializable]
    // public class GameSave
    // {
    //     public int playerLevel = 1;
    //     public int gold = 100;
    //     public string lastCheckpoint = "StartPoint";
    // }

    // [System.Serializable]
    // public class KeyBindings
    // {
    //     public string jumpKey = "Space";
    //     public string attackKey = "LeftMouse";
    // }

}