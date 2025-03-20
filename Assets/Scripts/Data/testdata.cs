using System;

[Serializable]
public class TestData : IGameData
{
    public string var1;
    public string var2;
    public string GetKey()
    {
        return typeof(TestData).Name;
    }
}