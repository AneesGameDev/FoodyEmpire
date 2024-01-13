using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainCoinsBAar : MonoBehaviour
{
    //public GameData gameData;
    //public CoinsControll coincontroller;
    public int coinsmain;
    public Text maincoins;
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        //gameData = FindObjectOfType<GameData>();
        // coincontroller = FindObjectOfType<CoinsControll>();
       // DontDestroyOnLoad(this.gameObject);

        gameData = FindObjectOfType<GameData>();
        //board = FindObjectOfType<Board>();
        if (gameData != null)
        {
            DontDestroyOnLoad(gameData);
            coinsmain = gameData.saveData.coins;
            maincoins.text = "" + coinsmain;
            //Debug.Log(numberOfCoins);
        }
         
           
      

    }

    // Update is called once per frame
    void Update()
    {
        /*        if (PlayerPrefs.HasKey("update"))
                {
                    if (PlayerPrefs.GetInt("update") == 1)
                    {
                        coinsmain = PlayerPrefs.GetInt("SetDataBaseCoins");
                        PlayerPrefs.SetInt("update", 0);
                    }
                    else if (coinsmain > 0)
                    {
                        maincoins.text = "" + coinsmain;
                    }
                }
                else if (coinsmain > 0) { 
                maincoins.text = "" +coinsmain;
                }*/
        if (PlayerPrefs.HasKey("coins"))
        {
            int fbcoins = PlayerPrefs.GetInt("coins");
            maincoins.text = "" + fbcoins;

}



        }
}
