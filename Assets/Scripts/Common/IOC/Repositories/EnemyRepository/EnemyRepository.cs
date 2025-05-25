using System.Collections.Generic;

using Models;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(EnemyRepository))]
    public class EnemyRepository : JsonRepositoryBase<EnemyData, EnemyJsonConfig>
    {
        protected override List<EnemyData> ExtractList(EnemyJsonConfig config)
        {
            return config.Enemies;
        }

        protected override string GetId(EnemyData data)
        {
            return data.ID;
        }

        protected override string GetName(EnemyData data)
        {
            return data.Name;
        }
    }
}