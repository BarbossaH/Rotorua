using Common;
using Game.SkillSystem;
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
        var id = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
        Debug.Log(id);
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
