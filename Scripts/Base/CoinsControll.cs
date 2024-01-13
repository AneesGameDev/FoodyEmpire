using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinsControll : MonoBehaviour
{
    public EngGameManager endGameManager;
    public int numberOfCoins;
    public GameData gameData;
    public Board board;
    public int level;
    public void nextLevel(){ 
        if(gameData!=null){

        
        numberOfCoins+=endGameManager.currentvalue;
        gameData.saveData.coins=numberOfCoins;
        gameData.saveData.IsActive[board.level+1]=true;
        //gameData.saveData.coins=numberOfCoins;
			if (endGameManager.currentvalue >= 2 && endGameManager.currentvalue<=3)
			{
                gameData.saveData.Stars[board.level] = 1;

            }
            if(endGameManager.currentvalue >=4 && endGameManager.currentvalue <= 5)
			{
                gameData.saveData.Stars[board.level] = 2;
            }
            if(endGameManager.currentvalue >=6 && endGameManager.currentvalue <= 50)
			{
                gameData.saveData.Stars[board.level] = 3;
            }

        gameData.Save();
            // PlayerPrefs.SetInt("coins", numberOfCoins);
            //Debug.Log(PlayerPrefs.GetInt("coins"));


           // board.nextlevel = true;
            SceneManager.LoadScene("2ndScreen");
            
        }
}
    public void cancelButton()
	{
        if (gameData != null)
        {


            numberOfCoins += endGameManager.currentvalue;
            gameData.saveData.coins = numberOfCoins;
            gameData.saveData.IsActive[board.level + 1] = true;
            gameData.saveData.coins = numberOfCoins;
            gameData.Save();
            // PlayerPrefs.SetInt("coins", numberOfCoins);
            //Debug.Log(PlayerPrefs.GetInt("coins"));


            // board.nextlevel = true;
            SceneManager.LoadScene("1stScene");
        }
       
	}
   /* public void NextLevelLoad(){
        if(PlayerPrefs.HasKey("Current Level")){
			level=PlayerPrefs.GetInt("Current Level");
            Debug.Log(level);
		}*/
   

    
    // Start is called before the first frame update
    void Start()
    {
        
        endGameManager=FindObjectOfType<EngGameManager>();
        gameData= FindObjectOfType<GameData>();
        board=FindObjectOfType<Board>();
        if(gameData!=null){
        DontDestroyOnLoad(gameData);
        numberOfCoins = gameData.saveData.coins;
        Debug.Log(numberOfCoins);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
