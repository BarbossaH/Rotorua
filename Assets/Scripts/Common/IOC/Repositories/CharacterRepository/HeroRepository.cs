using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    [AutoRegisterRepository(typeof(HeroRepository))]
    public class HeroRepository : JsonRepositoryBase<HeroData, HeroJsonConfig>
    {
        protected override List<HeroData> ExtractList(HeroJsonConfig config) => config.Heroes;
        protected override string GetId(HeroData data) => data.ID;
        protected override string GetName(HeroData data) => data.Name;

    }
}