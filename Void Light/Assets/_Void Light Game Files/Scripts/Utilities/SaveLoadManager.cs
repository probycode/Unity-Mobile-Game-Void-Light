using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

    public static void SavePlayer (PlayerData playerData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerData);

        bf.Serialize(stream,data);
        stream.Close();
        GameManager.OnGameSaved();
    }

    public static PlayerData LoadPlayer ()
    {
        if(File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        }
        else
        {
            GameManager.playerData.initailizedData = true;
            Debug.Log("No File Found Named: player.sav" + "Creating New Save File");
            return null;
        }
    }
}
