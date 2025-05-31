using System.Collections.Generic;

namespace Models
{
    public class MonsterJsonConfig : IGameData
    {
        public List<MonsterData> Monsters;
        public string GetKey()
        {
            return typeof(MonsterJsonConfig).Name;
        }
    }

    public class MonsterData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int MaxHP { get; set; }
        //some permanent passive skills
        public List<string> BuffIDs { get; set; }
        //actually this is active skills
        public List<string> IntentIDs { get; set; }

        public string DefaultIntentID { get; set; }
        public string IntentPicker { get; set; }
    }


}