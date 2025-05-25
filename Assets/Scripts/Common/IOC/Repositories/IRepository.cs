namespace Common
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;

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
}
