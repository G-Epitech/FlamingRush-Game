using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CacheSystem
{
    private static string getCachingPath()
    {
        return Application.persistentDataPath + "/test.sofr";
    }

    public static void saveTest()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = getCachingPath();
        FileStream fs = new FileStream(path, FileMode.Create);
        formatter.Serialize(fs, "dedezedzz");
        fs.Close();
    }

    public static string loadTest()
    {
        string result = null;
        string path = getCachingPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            result = formatter.Deserialize(fs) as string;
            fs.Close();
        }
        else
        {
            Debug.LogWarning("No cache found");
        }
        return result;
    }
}
