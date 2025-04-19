namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UnityEngine;

    public class EffectDataJson : IGameData
    {
        public EffectDataBase[] Effects;
        public string GetKey()
        {
            return typeof(EffectDataJson).Name;
        }
    }

    [JsonConverter(typeof(EffectParameterConverter))]
    public abstract class EffectDataBase
    {
        public string ID;
        public string Name;
        public string Element;

        public ElementEnum GetElementType() =>
        Enum.TryParse(Element, out ElementEnum result) ? result : ElementEnum.None;

        public virtual bool Validate(string skillName)
        {
            if (!UtilTools.ValidateEnum(Element, out EffectTypeEnum resultEffect, nameof(Element), skillName)) return false;

            return true;
        }
    }

    public class PhysicalDamageData : EffectDataBase
    {
        public float Damage;
    }

    public class FireballData : EffectDataBase
    {
        public float Damage;
    }

    public class IceArrowData : EffectDataBase
    {
        public float Damage;
    }

    public class DrawCardData : EffectDataBase
    {
        public int Amount;
    }

    public class MultipleAttackData : EffectDataBase
    {
        public float Amount;
    }
    public class MultipleDefenseData : EffectDataBase
    {
        public float Amount;
    }
    public class LifeStealData : EffectDataBase
    {
        public float Amount;
    }
    public class CounterAttackData : EffectDataBase
    {
        public float Damage;
        public float Chance;
    }
    #region newton soft json convert
    public class EffectParameterConverter : JsonConverter
    {
        // private static readonly Dictionary<string, Type> EffectTypeMap = new();
        // static EffectParameterConverter()
        // {
        //     // 获取所有 EffectParameterBase 的子类，并注册它们的类型映射
        //     var effectBaseType = typeof(EffectDataBase);
        //     var subclasses = Assembly.GetExecutingAssembly()
        //         .GetTypes()
        //         .Where(t => effectBaseType.IsAssignableFrom(t) && !t.IsAbstract);

        //     foreach (var subclass in subclasses)
        //     {
        //         EffectTypeMap[subclass.Name] = subclass; // using the name of the class as the key
        //     }
        // }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EffectDataBase);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string typeName = jsonObject["Name"]?.ToString() + "Data";

            if (string.IsNullOrEmpty(typeName))
            {
                throw new Exception("Missing or invalid SubclassName field in JSON.");
            }

            // Type effectType = Type.GetType(typeName);
            // if (effectType == null || !typeof(EffectDataBase).IsAssignableFrom(effectType))
            // {
            //     throw new Exception($"Unknown or invalid effect type: {typeName}");
            // }
            // Debug.Log(typeName);
            // var effect = Activator.CreateInstance(effectType);

            var effect = GameTypeCache.CreateInstanceByClassName<EffectDataBase>(typeName, throwError: true);

            serializer.Populate(jsonObject.CreateReader(), effect);
            return effect;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("Skill data should not be serialized back to JSON.");
            //because skill json don't need to be changed,so there is no need to write into the json file.
            // JObject jsonObject = JObject.FromObject(value);
            // jsonObject["SubclassName"] = value.GetType().Name;
            // jsonObject.WriteTo(writer);
        }
    }

    #endregion
}