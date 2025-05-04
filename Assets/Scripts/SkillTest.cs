
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        // 获取当前 GameObject 上的 Button 组件
        button = GetComponent<Button>();

        if (button != null)
        {
            // 绑定点击事件
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // Skill s = SkillManager.Instance.CreateSkill("SKL_10001", 0);
        // s?.ApplySkill();

        // // var name = IoCContainer.GetService<ICardRepository>().GetNameById("CRD_10001");
        // // Debug.Log(name);
        // var c = CardManager.Instance.CreateCard("CRD_10001");
        // Debug.Log(c.Name);
        // var h = HeroManager.Instance.CreateHero("Hero_101");
        // Debug.Log(h.Name);

        // Card card = CardManager.Instance.CreateCard("CRD_10001");

        // Debug.Log(card.CardId + " and " + card.CardInstanceId);
        // card = CardManager.Instance.CloneCard(card);
        // Debug.Log(card.CardId + " and " + card.CardInstanceId);
        _ = LoadingSetting();
    }
    private async UniTask LoadingSetting()
    {
        #region save game setting test
        // GameSettings gameSettings = new()
        // {
        //     MusicVolume = 0.1f,
        //     SFXVolume = 0.2f,
        //     Language = "Chinese"
        // };

        // bool result = await PersistentDataManager.Instance.SaveData(gameSettings, "game_setting.json");
        // Debug.Log($"setting file is {result}");
        #endregion

        #region player data test
        GameDataSave gameDataSave = new()
        {
            SaveID = "001",
            HeroSaveData = new HeroSaveData()
            {
                MaxHp = 200,
                CurrentHP = 100,
                MaxEnergy = 3,
                CurrentEnergy = 2,
                CardIDs = { "123", "124" }
            },
            RelicSaveData = {
                new RelicSaveData { ID = "1234" },
                new RelicSaveData(){ID="4321"},
            }
        };

        bool result = await PersistentDataManager.Instance.SaveData(gameDataSave, "game_data.json");
        Debug.Log($"setting file is {result}");
        #endregion

    }

    private void OnDestroy()
    {
        // 移除事件，避免内存泄漏
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
