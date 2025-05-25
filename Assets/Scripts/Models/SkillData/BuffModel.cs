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

    public abstract class TriggerBuffData : BuffDataBase
    {
        public string TriggerTiming;
        public TriggerTimingEnum GetTriggerTimingEnum() =>
       Enum.TryParse(TriggerTiming, out TriggerTimingEnum result) ? result : TriggerTimingEnum.None;
    }

    public abstract class StatusBuffData : BuffDataBase
    {
        public string StatusType;
        public BuffStatusTypeEnum GetBuffStatusTypeEnum() =>
        Enum.TryParse(StatusType, out BuffStatusTypeEnum result) ? result : BuffStatusTypeEnum.None;
    }
    public class FlameData : TriggerBuffData
    {
        public float Damage;
    }

    public class RejuvenationData : TriggerBuffData
    {
        public float HealOverTime;
    }
    public class ShiedData : StatusBuffData
    {
        public float AbsorbAmount;
    }
    public class MultipleDamageData : StatusBuffData
    {
        public float Multiplier;
    }
    public class FrozenData : StatusBuffData
    {
        public bool IsControlled;
    }
    #region BuffDataConverter
    public class BuffDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BuffDataBase);
        }

        public override bool CanWrite => false;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string typeName = jsonObject["Name"]?.ToString() + "Data";

            var buff = GameTypeCache.CreateInstanceByClassName<BuffDataBase>(typeName, throwError: true);

            serializer.Populate(jsonObject.CreateReader(), buff);
            return buff;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Buff data should not be serialized back to JSON.");
        }
    }
    #endregion
}