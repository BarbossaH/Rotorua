using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Models
{
    public class GameDataSave
    {
        public string SaveID { get; set; }
        public HeroSaveData HeroSaveData { get; set; }
        public List<RelicSaveData> RelicSaveData = new();
    }


    /// <summary>
    /// this class is for storing the dynamic data of hero for saving game progress.
    /// </summary>
    public class HeroSaveData
    {
        public float MaxHp { get; set; }
        public float CurrentHP { get; set; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set; }
        public List<string> CardIDs { get; set; } = new();
        // public List<string> RelicIDs { get; set; }=new();
    }
    public class RelicSaveData
    {
        public string ID { get; set; }
    }

    // public class SaveDataConverter : JsonConverter<ISaveData>
    // {
    //     public override ISaveData ReadJson(JsonReader reader, Type objectType, ISaveData existingValue, bool hasExistingValue, JsonSerializer serializer)
    //     {
    //         JObject obj = JObject.Load(reader);
    //         string typeName = obj["$type"]?.ToString();

    //         if (string.IsNullOrEmpty(typeName))
    //         {
    //             throw new JsonSerializationException("Missing $type property for ISaveData deserialization.");
    //         }

    //         var type = Type.GetType(typeName);
    //         if (type == null)
    //         {
    //             throw new JsonSerializationException($"Unknown type: {typeName}");
    //         }
    //         Debug.Log(typeName + 111111111);
    //         return (ISaveData)obj.ToObject(type, serializer);
    //     }

    //     public override void WriteJson(JsonWriter writer, ISaveData value, JsonSerializer serializer)
    //     {
    //         if (value == null)
    //         {
    //             writer.WriteNull();
    //             return;
    //         }

    //         JObject jo = JObject.FromObject(value, serializer); // ✅
    //         jo.AddFirst(new JProperty("$type", value.GetType().AssemblyQualifiedName));
    //         jo.WriteTo(writer);
    //     }

    // }
}