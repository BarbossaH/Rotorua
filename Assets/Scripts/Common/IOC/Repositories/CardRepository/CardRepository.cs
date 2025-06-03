using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(CardRepository))]
    public class CardRepository : JsonRepositoryBase<CardData, CardJsonConfig>
    {

        protected override List<CardData> ExtractList(CardJsonConfig config)
        {
            return config.Cards;
        }

        protected override string GetId(CardData data) => data.ID;


        protected override string GetName(CardData data) => data.Name;

    }
}