using System;
using System.Collections.Generic;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Models
{
    public class FilterConditionConfig : IGameData
    {
        public List<FilterDataBase> Filters;
        public string GetKey()
        {
            return typeof(FilterConditionConfig).Name;
        }
    }

    [JsonConverter(typeof(FilterConverter))]
    public class FilterDataBase
    {
        public string ID;
        public string ClassName { get; set; }
    }


    public class CoolDownFilterData : FilterDataBase
    {
        public int CoolDownTurns;
    }

    public class FirstTurnFilterData : FilterDataBase { }

    public class NotRepeatedFilterData : FilterDataBase
    {
        public int RepeatCoolDown;
    }

    public class FilterConverter : JsonConverter<FilterDataBase>
    {
        public override bool CanWrite => false;
        public override FilterDataBase ReadJson(JsonReader reader, Type objectType, FilterDataBase existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string className = jsonObject["ClassName"]?.ToString() + "Data";
            if (string.IsNullOrEmpty(className))
            {
                throw new JsonException("Missing 'ClassName' field for Filter.");
            }
            Debug.Log($"The data class name is {className}");
            var instance = GameTypeCache.CreateInstanceByClassName<FilterDataBase>(className, throwError: true);
            serializer.Populate(jsonObject.CreateReader(), instance);
            return instance;

        }

        public override void WriteJson(JsonWriter writer, FilterDataBase value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Filter data should not be serialized back to JSON.");
        }
    }
}