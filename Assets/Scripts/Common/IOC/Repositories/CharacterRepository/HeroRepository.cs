using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    [AutoRegisterRepository(typeof(IHeroRepository))]
    public class HeroRepository : IHeroRepository
    {
        private List<HeroData> _heroes = new();
        private readonly Dictionary<string, HeroData> _heroesDict = new();

        public bool IsLoaded { get; private set; }

        public List<HeroData> GetAll()
        {
            return _heroes;
        }

        public HeroData GetById(string id)
        {

            if (_heroesDict.TryGetValue(id, out var heroData))
            {
                return heroData;
            }
            Debug.LogError($"{id} cannot find her0 data");
            return null;
        }

        public string GetNameById(string id)
        {
            if (_heroesDict.TryGetValue(id, out var h))
            {
                return h.Name;
            }
            Debug.LogError($"{h.Name} does not exist");
            return null;
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;
            await JsonManager.Instance.WaitForJsonLoad();
            _heroes = JsonManager.Instance.GetData<HeroJsonConfig>(typeof(HeroJsonConfig).Name)?.Heroes;
            if (_heroes == null || _heroes.Count == 0)
            {
                Debug.LogError("Hero data loading failure");
                return;
            }


            foreach (var h in _heroes)
            {
                if (h == null)
                {
                    Debug.LogError("Found empty hero data! Skip!");
                    continue;
                }
                if (string.IsNullOrEmpty(h.ID))
                {
                    Debug.LogError($"Hero {h.Name}  ID is empty, Skip!");
                    continue;
                }

                Debug.Log($"Loaded Card: {h.ID} - {h.Name}");
                _heroesDict[h.ID] = h;
            }
            IsLoaded = true;
        }
    }
}