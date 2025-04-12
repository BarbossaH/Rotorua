using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IRepository<T, TKey> where T : class
{
    UniTask LoadDataAsync();
    T GetById(TKey id);
    List<T> GetAll();
    bool IsLoaded { get; }
}

public interface ISkillRepository : IRepository<SkillData, string>
{

}