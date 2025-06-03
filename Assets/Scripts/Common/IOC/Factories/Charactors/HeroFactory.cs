
namespace Common.Factories
{
    using Common.Repositories;
    using Game.Character;
    using UnityEngine;
    public interface IHeroFactory
    {
        Hero CreateHero(string id);
    }

    [AutoRegisterFactory(typeof(IHeroFactory))]
    public class HeroFactory : IHeroFactory
    {
        public Hero CreateHero(string heroId)
        {
            var data = IoCContainer.GetService<HeroRepository>()?.GetById(heroId);
            var heroSaveData = IoCContainer.GetService<IPlayerSaveRepository>()?.GetHeroSaveData("Save001");
            if (data == null)
            {
                Debug.LogError($"ID:{heroId} cannot be retrieved");
            }
            if (heroSaveData != null)
            {
                Debug.Log(heroSaveData?.StatusSaveData?.MaxHp);
            }
            return new Hero(data, heroSaveData);
        }


    }
}