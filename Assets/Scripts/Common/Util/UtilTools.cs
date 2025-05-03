using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public static class UtilTools
{
    public static void EnsureDictionary(string dic)
    {
        if (!Directory.Exists(dic))
        {
            Directory.CreateDirectory(dic);
        }
    }

    public static bool ValidateArray<T>(IList<T> array, string arrayName, string ownerName)
    {
        if (array == null)
        {
            Debug.LogError($"Error: {ownerName} -> {arrayName} is null.");
            return false;
        }
        if (array.Count == 0)
        {
            Debug.LogError($"Error: {ownerName} -> {arrayName} is empty.");
            return false;
        }
        for (int i = 0; i < array.Count; i++)
        {
            if (array[i] == null)
            {
                Debug.LogError($"Skill {ownerName} has a null value at index {i}!");
                return false;
            }
        }
        return true;
    }

    public static bool ValidateString(string str, string strName, string ownerName)
    {
        if (string.IsNullOrEmpty(str))
        {
            Debug.LogError($"{str} of {strName}is not valid in Skill {ownerName}");
            return false;
        }
        return true;
    }

    public static bool ValidatePositiveValue<T>(T amount, string valueName, string ownerName) where T : IComparable<T>
    {
        if (amount.CompareTo(default) < 0)
        {
            Debug.LogError($"Skill {ownerName}'s {valueName} value{amount} is less than 0");
            return false;
        }
        return true;
    }

    public static bool ValidateEnum<T>(string value, out T result, string fieldName, string ownerName) where T : struct, Enum
    {
        ValidateString(value, fieldName, ownerName);
        if (!Enum.TryParse(value, out result) || !Enum.IsDefined(typeof(T), result))
        {
            Debug.LogError($"Error: {ownerName} -> {fieldName} has an Invalid value {value}");
        }
        return true;
    }

    public static bool ValidateObject<T>(T obj, string fieldName, string ownerName) where T : class
    {
        if (obj == null)
        {
            Debug.LogError($"Error: {ownerName} -> {fieldName} is Null");
            return false;
        }
        return true;
    }

    public static void Shuffle<T>(List<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            //get an index between 0 - n (including n),note n already has been minus 1 at the above line code
            int k = rng.Next(n + 1);
            //swap the position of element k and element n, which means put a random element to the end of the list
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}