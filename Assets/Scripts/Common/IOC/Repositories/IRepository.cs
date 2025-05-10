namespace Common
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Models;

    public interface IDataRepository
    {
        UniTask LoadDataAsync();
        bool IsLoaded { get; }

    }

    public interface IRepository<T, TKey> : IDataRepository where T : class
    {
        T GetById(TKey id);
        List<T> GetAll();
        string GetNameById(string id);
    }


    public interface ISkillRepository : IRepository<SkillData, string> { }

    public interface ISkillEffectRepository : IRepository<EffectDataBase, string> { }

    public interface ISkillBuffRepository : IRepository<BuffDataBase, string> { }
    public interface ICardRepository : IRepository<CardData, string> { }

    //store the static data of hero, the data is original data
    public interface IHeroRepository : IRepository<HeroData, string /*hero id*/> { }
    //store the saving data of hero
    public interface IPlayerSaveRepository : IRepository<GameSlot, string/*save id*/>
    {
        HeroSaveData GetHeroSaveData(string saveId);
        List<RelicSaveData> GetRelicSaveData(string saveId);
        List<CardSaveData> GetCardSaveData(string saveId);
        HeroStatusSaveData GetHeroStatusSaveData(string saveId);
    }


    // public interface IEnemyRepository:IRepository<EnemyData,string>{}
}
