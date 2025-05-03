using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;

namespace Common
{
    [AutoRegisterRepository(typeof(ICardRepository))]
    public class CardRepository : ICardRepository
    {
        private readonly Dictionary<string, CardData> _cardDataDict = new();
        private List<CardData> _cardDataList = new();
        public bool IsLoaded { get; private set; }

        public List<CardData> GetAll()
        {
            return _cardDataList;
        }

        public CardData GetById(string id)
        {
            return _cardDataDict.TryGetValue(id, out var cardData) ? cardData : null;
        }

        public string GetNameById(string id)
        {
            if (_cardDataDict.TryGetValue(id, out var cardData))
            {
                return cardData.Name;
            }
            Debug.LogError("Card Name not exists");
            return null;
        }

        public async UniTask LoadDataAsync()
        {
            if (IsLoaded) return;
            await JsonManager.Instance.WaitForJsonLoad();
            _cardDataList = JsonManager.Instance.GetData<CardJsonConfig>(typeof(CardJsonConfig).Name)?.Cards;

            if (_cardDataList == null)
            {
                Debug.LogError("Card数据解析失败: Card 为空！");
                return;
            }
            if (_cardDataList.Count == 0)
            {
                Debug.LogError("Card数据为空，没有任何Card！");
                return;
            }

            foreach (var c in _cardDataList)
            {
                if (c == null)
                {
                    Debug.LogError("发现空card对象，跳过！");
                    continue;
                }
                if (string.IsNullOrEmpty(c.ID))
                {
                    Debug.LogError($"Card {c.Name} 的 ID 为空，跳过！");
                    continue;
                }

                Debug.Log($"Loaded Card: {c.ID} - {c.Name}");
                _cardDataDict[c.ID] = c;
            }

            IsLoaded = true;
        }
    }
}