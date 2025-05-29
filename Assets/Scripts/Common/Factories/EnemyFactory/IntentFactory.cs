using System.Collections.Generic;
using Common.Repositories;
using Game.Character.Monster;
using UnityEngine;

namespace Common.Factories
{
    public interface IIntentFactory
    {
        MonsterIntent CreateIntent(string intentId);
    }

    [AutoRegisterFactory(typeof(IIntentFactory))]
    public class IntentFactory : IIntentFactory
    {
        public MonsterIntent CreateIntent(string intentId)
        {
            //get intent id and the corresponding data
            var repo = IoCContainer.GetService<IntentRepository>();
            var data = repo.GetById(intentId);
            Debug.Log(data.Name);
            var filterFactory = IoCContainer.GetService<IFilterFactory>();
            List<IntentFilterBase> filters = new();
            foreach (var i in data.FilterIDs)
            {
                IntentFilterBase filter = filterFactory.CreateIntentFilter(i);

                if (filter != null)
                {
                    filters.Add(filter);
                }

            }
            return new MonsterIntent(data, filters);
        }
    }
}