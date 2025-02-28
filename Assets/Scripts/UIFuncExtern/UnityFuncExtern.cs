using UnityEngine;

public static class UnityFuncExtern
{
    public static T Find<T>(this GameObject parent, string childPath)
    {
        return parent.transform.Find(childPath).GetComponent<T>();
    }
}