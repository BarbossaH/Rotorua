using System;
using System.Collections.Generic;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Models
{
    public class BuffJsonConfig : IGameData
    {

        public List<BuffDataBase> Buffs;
        public string GetKey()
        {
            return typeof(BuffJsonConfig).Name;
        }
    }

    [JsonConverter(typeof(BuffDataConverter))]
    public abstract class BuffDataBase
    {
        public string ID;
        public string Name;
        public int Duration;
        public string Element;
        public ElementEnum GetElementType() =>
       Enum.TryParse(Element, out ElementEnum result) ? result : ElementEnum.None;
    }

    public class FlameData : BuffDataBase { }
    public class RejuvenationData : BuffDataBase { }
    public class ShiedData : BuffDataBase { }
    public class MultipleDamageData : BuffDataBase { }
    public class FrozenData : BuffDataBase { }
    #region BuffDataConverter
    public class BuffDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BuffDataBase);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string typeName = jsonObject["Name"]?.ToString() + "Data";

            if (string.IsNullOrEmpty(typeName))
            {
                throw new Exception("Missing or invalid SubclassName field in JSON.");
            }

            var effect = GameTypeCache.CreateInstanceByClassName<BuffDataBase>(typeName, throwError: true);

            serializer.Populate(jsonObject.CreateReader(), effect);
            return effect;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Buff data should not be serialized back to JSON.");
        }
    }
    #endregion
}