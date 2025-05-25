using Common.Repositories;
using Game.Character.Enemy;
using UnityEngine;

namespace Common.Factories
{
    public interface IFilterFactory
    {
        IntentFilterBase CreateIntentFilter(string filterId);
    }

    [AutoRegisterFactory(typeof(IFilterFactory))]
    public class FilterFactory : IFilterFactory
    {
        public IntentFilterBase CreateIntentFilter(string filterId)
        {
            var repo = IoCContainer.GetService<FilterRepository>();
            var data = repo.GetById(filterId);
            if (data == null)
            {
                Debug.LogError("Filter Data is empty"); ;
            }
            Debug.Log("Intent filter data's name is " + data.ClassName);
            IntentFilterBase filter = GameTypeCache.CreateInstanceByClassName<IntentFilterBase>(data.ClassName, true);
            if (filter != null)
            {
                filter.InitializeData(data);
            }
            return filter;
        }
    }
}