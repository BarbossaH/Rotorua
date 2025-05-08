using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Models
{
    public class PlayerDataSave
    {
        public List<GameSlot> GameSlots = new();
    }

    public class GameSlot
    {
        public string SaveID { get; set; }
        public HeroSaveData HeroSaveData { get; set; }
    }

    /// <summary>
    /// this class is for storing the dynamic data of hero for saving game progress.
    /// </summary>
    public class HeroSaveData
    {
        public HeroStatusSaveData StatusSaveData;
        public List<CardSaveData> CardInstanceData { get; set; } = new();
        public List<RelicSaveData> RelicSaveData = new();
    }
    public class HeroStatusSaveData
    {
        public float MaxHp { get; set; }
        public float CurrentHP { get; set; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set; }
    }
    public class CardSaveData
    {
        public string ID { get; set; }
        public int InstanceID { get; set; }
    }
    public class RelicSaveData
    {
        public string ID { get; set; }
    }


}