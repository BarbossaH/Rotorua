namespace Models
{
    [System.Serializable]
    public class PlayerData : IGameData
    {
        public string playerName = "Player";
        public int level = 1;
        public int experience = 0;
        public float playTime = 0;

        public string GetKey() => typeof(PlayerData).Name;

    }
}