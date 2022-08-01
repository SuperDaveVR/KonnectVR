using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class AvatarSaveSystem
{

    public static void SaveCustomSettings(Avatar avatar)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/avatarSettings.guy";
        FileStream stream = new FileStream(path, FileMode.Create);

        CustomSettings data = new CustomSettings(avatar);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CustomSettings LoadCustomSettings()
    {
        string path = Application.persistentDataPath + "/avatarSettings.guy";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CustomSettings data = (CustomSettings)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }    
    }
}
