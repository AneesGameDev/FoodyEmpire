using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject currentPanel;
    public int page;
    private GameData gameData;
    public int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        for(int i=0; i<panels.Length; i++)
		{
            panels[i].SetActive(false);
		}
		if (gameData != null)
		{
            for(int i= 0; i<gameData.saveData.IsActive.Length; i++)
			{
				if (gameData.saveData.IsActive[i])
				{
                   currentLevel = i;
                    PlayerPrefs.SetInt("preflevel", i );
                   // gameData.saveData.level = i;
                   // gameData.Save();
				}
			}
		}
        page = (int)Mathf.Floor(currentLevel / 9);
        currentPanel = panels[page];
        panels[page].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rightLevel()
	{
		if (page < panels.Length - 1)
		{
            currentPanel.SetActive(false);
            page++;
            currentPanel = panels[page];
            currentPanel.SetActive(true);
		}
	}
    public void leftLevel()
    {
        if (page >0)
        {
            currentPanel.SetActive(false);
            page--;
            currentPanel = panels[page];
            currentPanel.SetActive(true);
        }
    }
}
