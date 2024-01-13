using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum GameType{
    moves,
    time
}
[System.Serializable]
public class GameRequirements{
public GameType gameType;
public int countervalue;
}
public class EngGameManager : MonoBehaviour
{
    public Board board ;
    public bool clickstart=false;
    public GameObject MovesLabel;
    public GameObject TimeLabel;
    public Text counter;
    public GameRequirements endGameRequirements;
    public int currentvalue;
    public float timecount=1.0f;
    public GameObject panel;
    public bool moves=false;
    // Start is called before the first frame update
    void Start()
    {
        board=FindObjectOfType<Board>();
        SetGameType();
        currentvalue=endGameRequirements.countervalue;
        
       SetUp();
    }
    void SetGameType(){
        if(board.level<board.world.levels.Length){
        if(board.world!=null){
            if(board.world.levels[board.level]!=null){
            endGameRequirements= board.world.levels[board.level].endGameRequirements;
            }}
        }
    }
    void SetUp(){
         counter.text=""+endGameRequirements.countervalue;
        if(endGameRequirements.gameType==GameType.moves){
            MovesLabel.SetActive(true);
            TimeLabel.SetActive(false);

        }
        else {
             MovesLabel.SetActive(false);
            TimeLabel.SetActive(true);
        }
    }
    public void DecreaseCount(){
        if(currentvalue>0){
currentvalue--;
counter.text=""+currentvalue;

        }
        else{
            Debug.Log("You Lose");
        }
    }
    public  IEnumerator disablePanel()
	{  
        yield return new WaitForSeconds(.9f);
        panel.SetActive(true);


	}
public void onclick(){
    clickstart=true;
       StartCoroutine(disablePanel());
}
    // Update is called once per frame
    void Update()
    {
        if(clickstart){
        if(endGameRequirements.gameType==GameType.time){
  timecount-= Time.deltaTime;
if(timecount<=0){
    DecreaseCount();
    timecount=1.0f;
}
        }
    }}
/*	private void OnDisable()
	{
        panel.SetActive(true);
    }*/
}
