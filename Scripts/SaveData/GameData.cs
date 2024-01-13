using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SaveData
{
	public bool[] IsActive;
	public int[] HighScores;
	public int[] Stars;
	public int coins;


}
public class GameData : MonoBehaviour
{

	public static GameData gameData;
	public SaveData saveData;
	public bool createdata = false;
	// Start is called before the first frame update
	void Awake()
	{
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GameData"));

		if (gameData == null)
		{

			gameData = this;
		}
		else
		{
			Destroy(GameObject.FindGameObjectWithTag("GameData"));
		}
		if (PlayerPrefs.HasKey("update"))
		{
			Debug.Log("Update Value" + PlayerPrefs.GetInt("update"));
			if (PlayerPrefs.GetInt("update") == 1)
			{
				saveData = new SaveData();
				saveData.IsActive = new bool[10000];
				saveData.Stars = new int[10000];
				saveData.HighScores = new int[10000];
				saveData.IsActive[0] = true;
				Save();
				Load();
				PlayerPrefs.SetInt("update", 11);
				createdata = true;
				return;
			}
			else if (PlayerPrefs.GetInt("update") == 0)
			{
				if (File.Exists(Application.persistentDataPath + "/player.dat"))
				{
					if (createdata == false)
					{
						saveData = new SaveData();
						saveData.IsActive = new bool[10000];
						saveData.Stars = new int[10000];
						saveData.HighScores = new int[10000];
						saveData.IsActive[0] = true;
						createdata = true;

					}
					for (int i = 0; i < PlayerPrefs.GetInt("SetDataBaseLevel"); i++)
					{

						saveData.IsActive[i] = true;
					}
					saveData.coins = PlayerPrefs.GetInt("SetDataBaseCoins");
					Save();
					Load();
					PlayerPrefs.SetInt("update", 11);
					return;
				}
				else
				{
					saveData = new SaveData();
					saveData.IsActive = new bool[10000];
					saveData.Stars = new int[10000];
					saveData.HighScores = new int[10000];
					saveData.IsActive[0] = true;
					for (int i = 0; i < PlayerPrefs.GetInt("SetDataBaseLevel"); i++)
					{
						saveData.IsActive[i] = true;
					}
					saveData.coins = PlayerPrefs.GetInt("SetDataBaseCoins");
					Save();
					Load();
					PlayerPrefs.SetInt("update", 11);
					return;
				}
			}
		}

		Load();
	}

	public void Save()
	{
		BinaryFormatter formatter = new BinaryFormatter();

		FileStream file = File.Create(Application.persistentDataPath + "/player.dat");
		SaveData data = new SaveData();
		data = saveData;

		formatter.Serialize(file, data);

		file.Close();
		PlayerPrefs.SetInt("coins", saveData.coins);
		Debug.Log("Saved");
	}
	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/player.dat"))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
			saveData = formatter.Deserialize(file) as SaveData;
			file.Close();
			Debug.Log("Load");
		}
		else
		{
			saveData = new SaveData();
			saveData.IsActive = new bool[10000];
			saveData.Stars = new int[10000];
			saveData.HighScores = new int[10000];
			saveData.IsActive[0] = true;
			createdata = true;
			//saveData.level = 0;

		}

	}
	public void OnApplicationQuit()
	{
		Save();
	}

	public void OnDisable()
	{
		Save();
	}


	void Start()
	{

	}
	// Update is called once per frame
	void Update()
	{

	}
}
