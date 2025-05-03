using Common;
using Game.CardSystem;
using Game.SkillSystem;
using Managers;
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
        Skill s = SkillManager.Instance.CreateSkill("SKL_10001", 0);
        s?.ApplySkill();

        // var name = IoCContainer.GetService<ICardRepository>().GetNameById("CRD_10001");
        // Debug.Log(name);

        var name = IoCContainer.GetService<IHeroRepository>().GetNameById("Hero_101");
        Debug.Log(name);

        // Card card = CardManager.Instance.CreateCard("CRD_10001");

        // Debug.Log(card.CardId + " and " + card.CardInstanceId);
        // card = CardManager.Instance.CloneCard(card);
        // Debug.Log(card.CardId + " and " + card.CardInstanceId);
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
