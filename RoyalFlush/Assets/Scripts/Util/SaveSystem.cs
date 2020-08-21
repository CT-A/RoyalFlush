using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer (PlayerControlls player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            if (new FileInfo(path).Length == 0) {
                Debug.LogError("Cannot Load Empty Save :(");
                return null;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log(Application.persistentDataPath);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found :(");
            return null;
        }
    }

    public static bool CheckSave()
    {
        string path = Application.persistentDataPath + "/player.save";

        return (File.Exists(path) && (new FileInfo(path).Length != 0));
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
