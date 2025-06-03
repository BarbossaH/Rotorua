using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.CardSystem;
using Managers;
using Models;
using UnityEngine;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(SkillBuffRepository))]
    public class SkillBuffRepository : JsonRepositoryBase<BuffDataBase, BuffJsonConfig>
    {
        protected override List<BuffDataBase> ExtractList(BuffJsonConfig config) => config.Buffs;


        protected override string GetId(BuffDataBase data) => data.ID;


        protected override string GetName(BuffDataBase data) => data.Name;

    }
}