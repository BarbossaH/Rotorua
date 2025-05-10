using System;
using UnityEngine;

public static class UnityFuncExtern
{
    public static T Find<T>(this GameObject parent, string childPath)
    {
        return parent.transform.Find(childPath).GetComponent<T>();
    }
}

public enum SaveFileEnum
{
    Settings,
    PlayerData,
}

public static class SaveFileExtern
{
    public static string GetFileName(this SaveFileEnum file)
    {
        return file switch
        {
            SaveFileEnum.Settings => "game_setting.json",
            SaveFileEnum.PlayerData => "game_data.json",
            _ => throw new ArgumentOutOfRangeException(nameof(file))
        };
    }
}