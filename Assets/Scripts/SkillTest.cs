
using System.Collections.Generic;
using Common;
using Common.Factories;
using Common.Repositories;
using Cysharp.Threading.Tasks;
using Game.Battle;
using Game.CardSystem;
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
        // LoadingSetting();

        //test controller
        // testDataLoad();

        //create enemy
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        var factory = IoCContainer.GetService<IEnemyFactory>();
        var enemy = factory.CreateEnemy("MON_10001");
        Debug.Log($"enemy {enemy.Name} is created");
    }
    private void testDataLoad()
    {
        var repo = IoCContainer.GetService<EnemyRepository>();
        var data = repo.GetById("MON_10001");
        Debug.Log(data.Name);
    }
    private void testBattle()
    {
        var scope = IoCContainer.CreateScope();
        IBattleController ctr = scope.GetService<IBattleController>();
        ctr.StartBattle();
    }
    private void LoadingSetting()
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
        _ = TestPersistentData();
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

    #region test code
    private async UniTask TestPersistentData()
    {
        PersistentJsonConfig gameDataSave = new PersistentJsonConfig
        {
            GameSlots = {
                new PlayerSaveDataSlot{
                    SaveID = "Save001",
                    HeroSaveData  =  new HeroSaveData{
                        StatusSaveData=new HeroStatusSaveData{
                        MaxEnergy=9,
                        CurrentEnergy=7,
                        MaxHp=100,
                        CurrentHP=87,
                        },
                        CardInstanceData={ new CardSaveData{ID="C001", InstanceID=1}, new CardSaveData{ID="C002", InstanceID=2}},
                        RelicSaveData={new RelicSaveData{ID="R001"}}
                    }
                }
            }

        };

        bool result = await PersistentDataManager.Instance.SaveData(gameDataSave, "game_data.json");
        Debug.Log($"setting file is {result}");
    }
    #endregion
}
