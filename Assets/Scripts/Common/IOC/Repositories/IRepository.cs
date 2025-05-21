namespace Common
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Game.Character;
    using Models;

    //整个代码需要重构一下，这里最好使用基类或者抽象类去实现，因为这里面很多逻辑都是共同的
    public interface IDataRepository
    {
        UniTask LoadDataAsync();
        bool IsLoaded { get; }
    }

    public interface IRepository<T> : IDataRepository where T : class
    {

        T GetById(string id);
        List<T> GetAll();
        string GetNameById(string id);
    }


    // public interface ISkillRepository : IRepository<SkillData, string> { }

    // public interface ISkillEffectRepository : IRepository<EffectDataBase, string> { }

    // public interface ISkillBuffRepository : IRepository<BuffDataBase, string> { }
    // public interface ICardRepository : IRepository<CardData, string> { }
    // public interface IEnemyRepository : IRepository<EnemyData, string> { }
    // public interface IIntentRepository : IRepository<EnemyIntentData, string> { }
    //下面的代码有待商榷，因为是多态的，也许不需要写在这个类，这个跟
    // public interface IIntentFilterRepository : IRepository<FilterDataBase, string> { }

    //store the static data of hero, the data is original data
    // public interface IHeroRepository : IRepository<HeroData, string /*hero id*/> { }
    //store the saving data of hero



    // public interface IEnemyRepository:IRepository<EnemyData,string>{}
}
