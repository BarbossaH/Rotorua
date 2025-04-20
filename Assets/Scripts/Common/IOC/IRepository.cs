namespace Common
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Models;
    using Game.SkillSystem;

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
    //TODO:这里要研究一下是不是多余了。周一到周三正好研究一下自动注册 ioc container的知识
    public interface ISkillRepository : IRepository<SkillData, string> { }

    public interface ISkillEffectRepository : IRepository<EffectDataBase, string> { }

    public interface ISkillBuffRepository : IRepository<SkillBuffBase, string> { }
}
