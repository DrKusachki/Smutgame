using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
	playerStats playerData;
	string saveFilePath;
	void Start()
	{
		saveFilePath = Application.persistentDataPath + "/PlayerData.json";
	}

	playerStats getPlayerData()
	{
		return playerMain.Player.stats;	
	}

	void setPlayerData(playerStats stats)
	{
		playerMain.Player.stats = stats;
	}

	public void SaveGame()
	{
		playerData = getPlayerData();
		playerData.playerPosition = playerMain.Player.transform.position;
		string savePlayerData = JsonUtility.ToJson(playerData);
		File.WriteAllText(saveFilePath, savePlayerData);
		Debug.Log("YOU SAVED");
	}
	public void LoadGame()
	{
		if (File.Exists(saveFilePath))
		{
			string loadPlayerData = File.ReadAllText(saveFilePath);
			JsonUtility.FromJsonOverwrite(loadPlayerData, playerMain.Player.stats);

			Debug.Log($"Player info: {playerData.health} HP, " +
				$"{playerData.mana} MP, " +
				$"{playerData.souls} souls, " +
				$"{playerData.soulsPersistent} PSouls, " +
				$"player is on {playerData.playerPosition}");

			playerMain.Player.transform.position = playerData.playerPosition;
		}
		else
		{
			Debug.Log("Can't do it, chief, no file to load");
		}
	}
	public void DeleteSaveFile()
	{
		if (!File.Exists(saveFilePath))
		{
			File.Delete(saveFilePath);
			Debug.Log("Sic semper tyrannis");
		}
		else
		{
			Debug.Log("Ex nihilo nihil");
		}
	}
	// Update is called once per frame
  
}
