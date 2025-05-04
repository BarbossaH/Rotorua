using System;
using System.Collections.Generic;

namespace Models
{
    public class HeroJsonConfig : IGameData
    {
        public List<HeroData> Heroes;
        public string GetKey() => typeof(HeroJsonConfig).Name;

    }
    public class HeroData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public float MaxHP { get; set; }
        public int Energy { get; set; }
        public string Occupation { get; set; }

        public OccupationEnum GetOccupationEnum() =>
        Enum.TryParse(Occupation, out OccupationEnum result) ? result : OccupationEnum.None;


    }
}