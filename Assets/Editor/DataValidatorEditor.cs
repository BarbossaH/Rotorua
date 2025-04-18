using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Managers;
using Models;

public class DataValidatorEditor : EditorWindow
{
    [MenuItem("Tools/Validate Skill Data")]
    public static async void ValidateSkillJson()
    {
        bool isValid = true;
        await JsonManager.Instance.LoadJsonFile();
        await JsonManager.Instance.WaitForJsonLoad();
        List<SkillData> allSkills = JsonManager.Instance.GetData<SkillJsonConfig>(typeof(SkillJsonConfig).Name).Skills;
        foreach (var skill in allSkills)
        {
            if (!skill.Validate())
            {
                Debug.LogError($"Skill {skill.Name} is invalid");
                isValid = false;
            }
        }
        Debug.Log(isValid ? "All Skill data is valid" : "there is something wrong, please check!");
        if (isValid)
        {
            EditorUtility.DisplayDialog("Validation finished", "All good", "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("Validation finished", "Something Wrong", "OK");
        }
    }
}
