
using System.Collections.Generic;

namespace Models
{
    public class IntentJsonConfig : IGameData
    {
        public List<EnemyIntentData> Intents;
        public string GetKey()
        {
            return typeof(IntentJsonConfig).Name;
        }
    }
    public class EnemyIntentData
    {
        public string ID;
        public string Name;
        public List<string> Effects;
        public List<string> Buffs;
        public List<string> FilterIDs;
        public float MinValue;
        public float MaxValue;
        public int RepeatCount;
    }
}