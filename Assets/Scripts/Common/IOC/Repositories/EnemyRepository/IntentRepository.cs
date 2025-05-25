using System.Collections.Generic;
using Models;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(IntentRepository))]
    public class IntentRepository : JsonRepositoryBase<EnemyIntentData, IntentJsonConfig>
    {
        protected override List<EnemyIntentData> ExtractList(IntentJsonConfig config) => config.Intents;


        protected override string GetId(EnemyIntentData data) => data.ID;


        protected override string GetName(EnemyIntentData data) => data.Name;

    }
}