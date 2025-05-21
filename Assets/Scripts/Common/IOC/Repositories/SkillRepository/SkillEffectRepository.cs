//this class is for retrieving effect data and other methods to get specific data
//the data is stored in the IocContainer
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    [AutoRegisterRepository(typeof(SkillEffectRepository))]
    public class SkillEffectRepository : JsonRepositoryBase<EffectDataBase, EffectJsonConfig>
    {
        protected override List<EffectDataBase> ExtractList(EffectJsonConfig config) => config.Effects;


        protected override string GetId(EffectDataBase data) => data.ID;


        protected override string GetName(EffectDataBase data) => data.Name;

    }
}