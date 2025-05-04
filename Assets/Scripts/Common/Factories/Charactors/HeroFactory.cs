using Game.Character;
using UnityEngine;

namespace Common
{
    public interface IHeroFactory
    {
        Hero CreateHero(string id);
    }

    [AutoRegisterFactory(typeof(IHeroFactory))]
    public class HeroFactory : IHeroFactory
    {
        public Hero CreateHero(string heroId)
        {
            var data = IoCContainer.GetService<IHeroRepository>()?.GetById(heroId);
            if (data == null)
            {
                Debug.LogError($"ID:{heroId} cannot be retrieved");
            }
            return new Hero(data);
        }


    }
}