using Common;
using Game.Character;

namespace Managers
{
    [AutoRegisterManager]
    public class HeroManager : Singleton<HeroManager>
    {
        private readonly IHeroFactory _heroFactory;
        public HeroManager()
        {
            _heroFactory = IoCContainer.GetService<IHeroFactory>();
        }

        public Hero CreateHero(string id)
        {
            return _heroFactory.CreateHero(id);
        }
    }
}