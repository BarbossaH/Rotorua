using System.Collections.Generic;
using Models;

namespace Common.Repositories
{
    [AutoRegisterRepository(typeof(FilterRepository))]
    public class FilterRepository : JsonRepositoryBase<FilterDataBase, FilterConditionConfig>
    {
        protected override List<FilterDataBase> ExtractList(FilterConditionConfig config)
        {
            return config.Filters;
        }

        protected override string GetId(FilterDataBase data)
        {
            return data.ID;
        }

        protected override string GetName(FilterDataBase data)
        {
            return data.ClassName;
        }
    }
}