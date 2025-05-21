using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;

namespace Common
{
    public abstract class JsonRepositoryBase<TData, TConfig> : IRepository<TData>
    where TData : class
    where TConfig : class, Models.IGameData
    {
        protected readonly List<TData> _dataList = new();

        //string is the id of the corresponding data
        protected readonly Dictionary<string, TData> _dataDict = new();
        public bool IsLoaded { get; private set; }

        public List<TData> GetAll() => _dataList;
        public TData GetById(string id) => _dataDict.TryGetValue(id, out var data) ? data : null;
        public string GetNameById(string id)
        {
            var data = GetById(id);
            if (data != null)
            {
                return GetName(data);
            }
            Debug.LogError($"Data id is {id} does not exist.");
            return null;
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;
            //to ensure the data has been loaded;
            await JsonManager.Instance.WaitForJsonLoad();
            var config = JsonManager.Instance.GetData<TConfig>(typeof(TConfig).Name);
            if (config == null)
            {
                Debug.LogError($"Data parsing failed:{typeof(TConfig).Name}");
            }

            var list = ExtractList(config);
            if (list == null || list.Count == 0)
            {
                Debug.LogError($"Data{typeof(TData).Name} is null");
                return;
            }

            foreach (var item in list)
            {
                if (item == null)
                {
                    Debug.LogError($"Object of {typeof(TData).Name} is null");
                    continue;
                }
                var id = GetId(item);
                if (string.IsNullOrEmpty(id))
                {
                    Debug.LogError($"Id of {GetName(item)} is empty");
                    continue;
                }

                _dataList.Add(item);
                _dataDict[id] = item;

                Debug.Log($"The data of {typeof(TData).Name} has been loaded");
                IsLoaded = true;
            }
        }

        protected abstract string GetName(TData data);
        protected abstract string GetId(TData data);
        protected abstract List<TData> ExtractList(TConfig config);
    }
}