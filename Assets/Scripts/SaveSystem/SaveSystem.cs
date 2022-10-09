using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    //data for a save file
    public static void SaveData(string filename, SaveInventoryItems saveItems)//InkController script,
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + "saves" + "/" + filename + ".data";
        string folderpath = Application.persistentDataPath + "/" + "saves";

        if (Directory.Exists(folderpath))
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            SaveData data = new SaveData(saveItems);//script
            formatter.Serialize(stream, data);
            stream.Close();

        }
        else
        {
            Directory.CreateDirectory(folderpath);
            FileStream stream = new FileStream(path, FileMode.Create);
            SaveData data = new SaveData(saveItems);//script
            formatter.Serialize(stream, data);
            stream.Close();
        }

    }

    public static SaveData LoadData(string filename)
    {
        string path = Application.persistentDataPath + "/" + "saves" + "/" + filename + ".data";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

}
