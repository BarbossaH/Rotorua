
using System.Collections.Generic;

namespace Models
{
    public class IntentJsonConfig : IGameData
    {
        public List<MonsterIntentData> Intents;
        public string GetKey()
        {
            return typeof(IntentJsonConfig).Name;
        }
    }
    public class MonsterIntentData
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