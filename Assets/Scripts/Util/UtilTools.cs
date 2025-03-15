using System.IO;
public static class UtilTools
{
    public static void EnsureDictionary(string dic){
        if(!Directory.Exists(dic))
        {
            Directory.CreateDirectory(dic);
        }
    }
}