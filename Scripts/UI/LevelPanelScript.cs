using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPanelScript : MonoBehaviour
{
    //2feet bar , 12 feet long , samll dabi , Steel
    public bool IsActive;
    public Sprite ActiveSprite;
    public Sprite LockedSprite;
    public Image ButtonImage;
    public Button MyButton;
    public string LevelToLoad = "Main";
    public Text LevelButtonText;
    public int Level;
    public GameObject ConfirmPanel;
    public Image[] Stars;
    private GameData gameData;
    public int starsActive;

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        ButtonImage = GetComponent<Image>();
        MyButton = GetComponent<Button>();
        LoadData();
        decideSprite();
        displayLevelText();
        ActivateStars();
    }
    void LoadData()
    {
        if (gameData != null)
        {
			// Debug.Log(gameData.saveData.Stars[Level - 1]);
			if (Level > 0) {
                //Debug.Log(Level);
               //Debug.Log("Level is Tru or not in LevelPanelScript"+gameData.saveData.IsActive[Level - 1]);
                //gameData.saveData.IsActive[0] = true;
            if (gameData.saveData.IsActive[Level - 1])
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
            }
            //Debug.Log(gameData.saveData.Stars[Level - 1]);
            starsActive = gameData.saveData.Stars[Level - 1];
        }

    }
    void ActivateStars()
    {

        for (int i = 0; i < starsActive; i++)
        {
            Debug.Log("Stars Active is" +starsActive);
            Stars[i].enabled = true;
        }
    }

    public void decideSprite()
    {
        if (IsActive)
        {
            ButtonImage.sprite = ActiveSprite;
            MyButton.enabled = true;
            LevelButtonText.enabled = true;

        }
        else
        {
            ButtonImage.sprite = LockedSprite;
            MyButton.enabled = false;
            LevelButtonText.enabled = true;

        }
    }
    public void displayLevelText()
    {
        LevelButtonText.text = "" + Level;
    }
    public void confirmPanel()
    {
        PlayerPrefs.SetInt("Current Level", Level - 1);
        SceneManager.LoadScene(LevelToLoad);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
