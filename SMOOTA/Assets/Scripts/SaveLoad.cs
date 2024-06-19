using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    public int health;
    public int souls;
    public int solidsouls;
    public Vector3 position;
}

public class SaveLoad : MonoBehaviour
{
    PlayerData playerData;
    string saveFilePath;
    void Start()
    {
        playerData = new PlayerData();
        playerData.health = 100;
        playerData.souls = 5;
        playerData.solidsouls = 3;
        playerData.position = Vector3.zero;
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) { }
    }
    public void SaveGame()
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);
        Debug.Log("YOU SAVED");
    }
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
            Debug.Log("Load game complete! \nPlayer health:" + playerData.health + 
                ", Player souls:" + playerData.souls + 
                ", Player solidsouls:" + playerData.solidsouls + 
                ", Player Position:" + playerData.position);
        }
        else
        {
            Debug.Log("You, stupid bollock, there is nothing to load!");
        }
    }
    public void DeleteSaveFile()
    {
        if (!File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("YOU HAVE DELETED YOUR SAVE, NOW FACE YOUR DOOM");
        }
        else
        {
            Debug.Log("YOU CAN'T DELETE SOMETHING THAT DOES NOT EXIST");
        }
    }
    // Update is called once per frame
  
}
