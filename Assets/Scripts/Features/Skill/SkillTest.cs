using UnityEngine;

public class SkillTest : MonoBehaviour
{
    private void Start()
    {
        SkillDatabase skillDatabase = JsonManager.Instance.GetData<SkillDatabase>("SkillDatabase");
        // 访问 Fireball 技能
        SkillData fireball = skillDatabase.Skills[0];

        Debug.Log($"技能名称: {fireball.Name}");
        Debug.Log($"技能等级 1 的冷却时间: {fireball.Levels[0].CoolDown}");
    }
}
