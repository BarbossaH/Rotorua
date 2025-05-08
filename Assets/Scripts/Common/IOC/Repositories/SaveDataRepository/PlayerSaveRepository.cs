using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    [AutoRegisterRepository(typeof(IPlayerSaveRepository))]
    public class PlayerSaveRepository : IPlayerSaveRepository
    {
        public bool IsLoaded { get; private set; }

        private List<PlayerDataSave> _playerSaveData = new();
        private readonly Dictionary<string, PlayerDataSave> _playerSaveDataDict = new();

        public List<PlayerDataSave> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public PlayerDataSave GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public string GetNameById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;
            PlayerDataSave data = await PersistentDataManager.Instance.LoadData<PlayerDataSave>("game_data.json");
            if (data != null && data.GameSlots != null && data.GameSlots.Count >= 1)
            {
                Debug.Log("Already fetched the data from the persistent data json file");
            }
        }
    }
}