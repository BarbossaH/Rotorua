using System.Collections.Generic;
using Models;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(IntentRepository))]
    public class IntentRepository : JsonRepositoryBase<MonsterIntentData, IntentJsonConfig>
    {
        protected override List<MonsterIntentData> ExtractList(IntentJsonConfig config) => config.Intents;


        protected override string GetId(MonsterIntentData data) => data.ID;


        protected override string GetName(MonsterIntentData data) => data.Name;

    }
}