using System.Collections.Generic;

namespace Models
{
    public class EnemyJsonConfig : IGameData
    {
        public List<EnemyData> Enemies;
        public string GetKey()
        {
            return typeof(EnemyJsonConfig).Name;
        }
    }

    public class EnemyData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public List<string> BuffIDs { get; set; }
        public List<string> IntentIDs { get; set; }

        public string DefaultIntentID { get; set; }
        public string IntentPicker { get; set; }
    }
}