using System.Collections.Generic;

using Models;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(EnemyRepository))]
    public class EnemyRepository : JsonRepositoryBase<MonsterData, MonsterJsonConfig>
    {
        protected override List<MonsterData> ExtractList(MonsterJsonConfig config)
        {
            return config.Monsters;
        }

        protected override string GetId(MonsterData data)
        {
            return data.ID;
        }

        protected override string GetName(MonsterData data)
        {
            return data.Name;
        }
    }
}