using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CacheSystem
{
    private static string getCachingPath()
    {
        return Application.persistentDataPath + "/playerData.sofr";
    }

    public static void savePlayerData(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = getCachingPath();
        FileStream fs = new FileStream(path, FileMode.Create);
        formatter.Serialize(fs, playerData);
        fs.Close();
    }

    public static PlayerData loadPlayerData()
    {
        PlayerData result = new PlayerData();
        string path = getCachingPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            result = formatter.Deserialize(fs) as PlayerData;
            fs.Close();
        }
        else
        {
            result.name = "John Doe";
            result.profilePicture = "Image";
            result.id = "no_id";
            Debug.LogWarning("No cache found");
        }
        return result;
    }
}
