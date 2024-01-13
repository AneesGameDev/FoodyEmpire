//using System.Reflection.Metadata;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState{
    wait,
    move
}

public enum TileKind
{
	Breakable,
    Blank,
    Normal,
	Lock,
	Concrete,
}
public class MatchType
{
	public int type;
	public string color;
}
[System.Serializable]
public class TileType{
	public int x;
	public int y;
	public TileKind tileKind;
}

public class Board : MonoBehaviour {
	
    public int escapevalue=0;
	public Text WinCoinsBarText;
	public Text WinCoinsText;
	public Text PlusCoins;
	public world world;
	public int level;
	public Image chanceBar;
	public int chance;
	public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offSet;

	[Header("prefabs")]

    public GameObject tilePrefab;
	public GameObject breakableTilePrefab;
	public GameObject lockTilePrefab;
	public GameObject concreteTilePrefab;
	public GameObject[] dots;
    public GameObject destroyParticle;

	[Header("layout")]

	public TileType[] boardLayout;
    public bool[,] blankSpaces;
	public BackgroundTile[,] breakableTiles;
    public GameObject[,] allDots;
    public Dot currentDot;
	public BackgroundTile[,] lockTiles;
	public BackgroundTile[,] concreteTiles;

	[Header("MatchStuff")]

	private FindMatches findMatches;
	public int basePieceValue = 20;
	private int streakValue = 1;
	private ScoreManager scoreManager;
	private SoundManager soundManager;
	public float refillDelay = 0.5f;
	public int scoreGoals;
    public int totalchance;
    public GameObject AddsDialog;
    public GameObject StartDialog;
    public bool handle=false;
    public EngGameManager endGameManager;
    public GameObject youwindialog;
    public CoinsControll coincontroller;
    public Text DisplayScoreGoal;
	public bool nextlevel = false;
	public int temp;

public void Update(){
 
	 //
		 if(endGameManager.endGameRequirements.gameType == GameType.moves){
			 //for(int i=0; i<endGameManager.currentvalue; i++){
//escapevalue++;
			// }
			WinCoinsText.text=""+coincontroller.numberOfCoins;
			WinCoinsBarText.text = ""+coincontroller.numberOfCoins;
			 PlusCoins.text="+"+endGameManager.currentvalue;
		// 
		if(scoreManager.score>=scoreGoals){
		youwindialog.SetActive(true);
		}
	}else if(endGameManager.endGameRequirements.gameType == GameType.time)
		{
			WinCoinsText.text = "" + coincontroller.numberOfCoins;
			WinCoinsBarText.text = "" + coincontroller.numberOfCoins;
			PlusCoins.text = "" +endGameManager.currentvalue;
			if (scoreManager.score >= scoreGoals)
			{
				endGameManager.clickstart = false;
				youwindialog.SetActive(true);
			}
		}
	if(handle){
		StartCoroutine(AddsDialogMethod());
	}
/*
		if (findMatches.currentMatches.Count == 3)
		{
			Debug.Log(findMatches.currentMatches.Count);
			findMatches.CheckBombs();
		}*/


	}
public IEnumerator AddsDialogMethod(){
yield return new WaitForSeconds(.9f);
if(endGameManager.currentvalue<=0 && scoreManager.score!=scoreGoals ){
    AddsDialog.SetActive(true);
	handle = false;
	}
}
	// Use this for initialization
	private void Awake()
	{
		/*int temp = PlayerPrefs.GetInt("Current Level");
		if (PlayerPrefs.HasKey("Current Level")){
			if (nextlevel)
			{
				temp += 1;
				level = temp;
				nextlevel = false;
			}
			else { 
			level=PlayerPrefs.GetInt("Current Level");
			}
		}*/
		if (PlayerPrefs.HasKey("Current Level"))
		{
			level = PlayerPrefs.GetInt("Current Level");

			
		}

		if (world!=null){
			if (level < world.levels.Length)
			{
				if (world.levels[level]!=null){
			width= world.levels[level].width;
			height=world.levels[level].height;
			dots= world.levels[level].dots;
			boardLayout=world.levels[level].boardLayout;
			scoreGoals= world.levels[level].scores;
		}
		}
		}
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	void Start () {
		DisplayScoreGoal.text = ""+scoreGoals;
		endGameManager=FindObjectOfType<EngGameManager>();
		AddsDialog.SetActive(false);
		StartDialog.SetActive(true);
		soundManager = FindObjectOfType<SoundManager>();
		coincontroller=FindObjectOfType<CoinsControll>();
		scoreManager = FindObjectOfType<ScoreManager>();
		breakableTiles = new BackgroundTile[width, height];
        findMatches = FindObjectOfType<FindMatches>();
		blankSpaces = new bool[width, height];
        allDots = new GameObject[width, height];
		lockTiles = new BackgroundTile[width, height];
		concreteTiles = new BackgroundTile[width, height];
		SetUp();
		 
	}
    

	public void GenerateBlankSpaces(){
		for (int i = 0; i < boardLayout.Length; i ++)
		{
			if(boardLayout[i].tileKind == TileKind.Blank)
			{
				blankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
			}
		}
	}

    public void GenerateBreakableTiles()
	{
		//Look at all the tiles in the layout
		for (int i = 0; i < boardLayout.Length; i ++)
		{
			//if a tile is a "Jelly" tile
			if(boardLayout[i].tileKind == TileKind.Breakable)
			{
				//Create a "Jelly" tile at that position;
				Vector2 tempPosition = new Vector2(boardLayout[i].x, boardLayout[i].y);
				GameObject tile = Instantiate(breakableTilePrefab,tempPosition, Quaternion.identity);
				breakableTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
			}
		}
	}
	public void GenerateLockTiles()
	{
		//Look at all the tiles in the layout
		for (int i = 0; i < boardLayout.Length; i++)
		{
			//if a tile is a "Lock" tile
			if (boardLayout[i].tileKind == TileKind.Lock)
			{
				//Create a "Lock" tile at that position;
				Vector2 tempPosition = new Vector2(boardLayout[i].x, boardLayout[i].y);
				GameObject tile = Instantiate(lockTilePrefab, tempPosition, Quaternion.identity);
				lockTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
			}
		}
	}

	public void GenerateConcreteTiles()
	{
		//Look at all the tiles in the layout
		for (int i = 0; i < boardLayout.Length; i++)
		{
			//if a tile is a "Lock" tile
			if (boardLayout[i].tileKind == TileKind.Concrete)
			{
				//Create a "Lock" tile at that position;
				Vector2 tempPosition = new Vector2(boardLayout[i].x, boardLayout[i].y);
				GameObject tile = Instantiate(concreteTilePrefab, tempPosition, Quaternion.identity);
				concreteTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
			}
		}
	}

	/* public void chancemove()
	{
		 
		chance--;
		chanceText.text = "" + chance;
chanceBar.fillAmount = (float)chance/(float) totalchance; */
	/* switch (chance)
	{
		case 36: // if a is an integer
			chanceBar.fillAmount = 0.9f;
			break;
		case 32: // if a is a string
			chanceBar.fillAmount = 0.8f;
			break;
		case 28: // if a is an integer
			chanceBar.fillAmount = 0.7f;
			break;
		case 24: // if a is a string
			chanceBar.fillAmount = 0.6f;
			break;
		case 20: // if a is an integer
			chanceBar.fillAmount = 0.5f;
			break;
		case 16: // if a is a string
			chanceBar.fillAmount = 0.4f;
			break;
		case 12: // if a is an integer
			chanceBar.fillAmount = 0.3f;
			break;
		case 8: // if a is a string
			chanceBar.fillAmount = 0.2f;
			break;
		case 4: // if a is an integer
			chanceBar.fillAmount = 0.1f;
			break;
		case 2: // if a is a string
			chanceBar.fillAmount = 0.06f;
			break;
		case 0:
			Debug.Log("none of the above");
			break;
	} */
	/* } */
	private void SetUp(){
		//chanceBar.fillAmount = 1;
		//chanceText.text = "" + chance;
		GenerateBlankSpaces();
		GenerateBreakableTiles();
		GenerateLockTiles();
		GenerateConcreteTiles();
		for (int i = 0; i < width; i ++){
			for (int j = 0; j < height; j++)
			{
				if (!blankSpaces[i, j] && !concreteTiles[i, j])
				{
					Vector2 tempPosition = new Vector2(i, j + offSet);
					Vector2 tilePosition = new Vector2(i, j);
					GameObject backgroundTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
					backgroundTile.transform.parent = this.transform;
					backgroundTile.name = "( " + i + ", " + j + " )";

					int dotToUse = Random.Range(0, dots.Length);

					int maxIterations = 0;

					while (MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100)
					{
						dotToUse = Random.Range(0, dots.Length);
						maxIterations++;
						//Debug.Log(maxIterations);
					}
					maxIterations = 0;

					GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
					dot.GetComponent<Dot>().row = j;
					dot.GetComponent<Dot>().column = i;
					dot.transform.parent = this.transform;
					dot.name = "( " + i + ", " + j + " )";
					allDots[i, j] = dot;
				}
			}

        }
    }

    private bool MatchesAt(int column, int row, GameObject piece){
        if(column > 1 && row > 1){
			if (allDots[column - 1, row] != null && allDots[column - 2, row] != null)
			{
				if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
				{
					return true;
				}
			}
			if (allDots[column, row - 1] != null && allDots[column, row - 2] != null)
			{
				if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
				{
					return true;
				}
			}

        }else if(column <= 1 || row <= 1){
            if(row > 1){
				if (allDots[column, row - 1] != null && allDots[column, row - 2] != null)
				{
					if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
					{
						return true;
					}
				}
            }
            if (column > 1)
            {
				if (allDots[column - 1, row] != null && allDots[column - 2, row] != null)
				{
					if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
					{
						return true;
					}
				}
            }
        }

        return false;
    }

    
  private int ColumnOrRow(){
     List<GameObject> matchCopy= findMatches.currentMatches as List<GameObject>;
     for(int i=0; i<matchCopy.Count; i++){
		 Dot thisDot = matchCopy[i].GetComponent<Dot>();
		 int column = thisDot.column;
		 int row= thisDot.row;
		 int columnMatch = 0;
		 int rowMatch = 0;
		 for(int j=0; j < matchCopy.Count; j++){
			 Dot nextDot = matchCopy[j].GetComponent<Dot>();
			 if(nextDot == thisDot){
				 continue;
			 }
			 if(nextDot.column == thisDot.column && nextDot.CompareTag(thisDot.tag)){
				 columnMatch++;
			 }
			 if(nextDot.row == thisDot.row && nextDot.CompareTag(thisDot.tag)){
				 rowMatch++;
			 }

		 }
		 if(columnMatch==4 || rowMatch==4){
			 return 1;
		 }
		 else if(columnMatch==2 && rowMatch==2){
			 return 2;
		 }
		 else if(columnMatch==3 || rowMatch==3){
			 return 3;
		 }
	 }


      return 0;
	  /* !!!: alert 
        int numberHorizontal = 0;
        int numberVertical = 0;
        Dot firstPiece = findMatches.currentMatches[0].GetComponent<Dot>();
        if (firstPiece != null)
        {
            foreach (GameObject currentPiece in findMatches.currentMatches)
            {
                Dot dot = currentPiece.GetComponent<Dot>();
                if(dot.row == firstPiece.row){
                    numberHorizontal++;
                }
                if(dot.column == firstPiece.column){
                    numberVertical++;
                }
            }
        }
        return (numberVertical == 5 || numberHorizontal == 5);
*/
    }

     private void CheckToMakeBombs(){
    if(findMatches.currentMatches.Count>3){
		  int typeOfMatch = ColumnOrRow();
		  if(typeOfMatch == 1){

			     //Make a color bomb
                //is the current dot matched?
                if(currentDot != null){
                    if(currentDot.isMatched){
                        if(!currentDot.isColorBomb){
                            currentDot.isMatched = false;
                            currentDot.MakeColorBomb();
                        }
                    }else{
                        if(currentDot.otherDot != null){
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if(otherDot.isMatched){
                                if(!otherDot.isColorBomb){
                                    otherDot.isMatched = false;
                                    otherDot.MakeColorBomb();
                                }
                            }
                        }
                    }
                }
		  }
		  else if(typeOfMatch == 2){
			                  
				if (currentDot != null)
                {
                    if (currentDot.isMatched)
                    {
                        if (!currentDot.isAdjacentBomb)
                        {
                            currentDot.isMatched = false;
                            currentDot.MakeAdjacentBomb();
                        }
                    }
                    else
                    {
                        if (currentDot.otherDot != null)
                        {
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if (otherDot.isMatched)
                            {
                                if (!otherDot.isAdjacentBomb)
                                {
                                    otherDot.isMatched = false;
                                    otherDot.MakeAdjacentBomb();
                                }
                            }
                        }
                    }
                }

		  }
		  else if (typeOfMatch == 3)
		  {
			  findMatches.CheckBombs();
		  } 
	}


		 /*
        if(findMatches.currentMatches.Count == 4 || findMatches.currentMatches.Count == 7){
            findMatches.CheckBombs();
        }
        if(findMatches.currentMatches.Count ==5  || findMatches.currentMatches.Count == 8){
            if(ColumnOrRow()){
                //Make a color bomb
                //is the current dot matched?
                if(currentDot != null){
                    if(currentDot.isMatched){
                        if(!currentDot.isColorBomb){
                            currentDot.isMatched = false;
                            currentDot.MakeColorBomb();
                        }
                    }else{
                        if(currentDot.otherDot != null){
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if(otherDot.isMatched){
                                if(!otherDot.isColorBomb){
                                    otherDot.isMatched = false;
                                    otherDot.MakeColorBomb();
                                }
                            }
                        }
                    }
                }
            }else{
                //Make a adjacent bomb
                //is the current dot matched?
                if (currentDot != null)
                {
                    if (currentDot.isMatched)
                    {
                        if (!currentDot.isAdjacentBomb)
                        {
                            currentDot.isMatched = false;
                            currentDot.MakeAdjacentBomb();
                        }
                    }
                    else
                    {
                        if (currentDot.otherDot != null)
                        {
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if (otherDot.isMatched)
                            {
                                if (!otherDot.isAdjacentBomb)
                                {
                                    otherDot.isMatched = false;
                                    otherDot.MakeAdjacentBomb();
                                }
                            }
                        }
                    }
                }
            }
        }
		*/
    }


	public void BombRow(int row)
	{
		for (int i = 0; i < width; i++)
		{
			if (concreteTiles[i, row])
			{
				concreteTiles[i, row].TakeDamage(1);
				if (concreteTiles[i, row].hitPoints <= 0)
				{
					concreteTiles[i, row] = null;
				}
			}
		}
	}

	public void BombColumn(int column)
	{
		for (int i = 0; i < width; i++)
		{
			if (concreteTiles[column, i])
			{
				concreteTiles[column, i].TakeDamage(1);
				if (concreteTiles[column, i].hitPoints <= 0)
				{
					concreteTiles[column, i] = null;
				}
			}

		}
	}
	private void DestroyMatchesAt(int column, int row){
        if(allDots[column, row].GetComponent<Dot>().isMatched){
           

            //Does a tile need to break?
			if(breakableTiles[column, row]!=null)
			{
				//if it does, give one damage.
				breakableTiles[column, row].TakeDamage(1);
				if(breakableTiles[column, row].hitPoints <= 0)
				{
					breakableTiles[column, row] = null;
				}
                
			}
			if (lockTiles[column, row] != null)
			{
				//if it does, give one damage.
				lockTiles[column, row].TakeDamage(1);
				if (lockTiles[column, row].hitPoints <= 0)
				{
					lockTiles[column, row] = null;
				}
			}
			DamageConcrete(column, row);
			//Does the sound manager exist?
			if (soundManager != null)
			{
				soundManager.PlayRandomDestroyNoise();
			}
            GameObject particle = Instantiate(destroyParticle, 
            allDots[column, row].transform.position, 
            Quaternion.identity);
            Destroy(particle, .5f);
            Destroy(allDots[column, row]);
			scoreManager.IncreaseScore(basePieceValue * streakValue);
            allDots[column, row] = null;
        }
    }

    public void DestroyMatches(){
		 //How many elements are in the matched pieces list from findmatches?
            if(findMatches.currentMatches.Count >= 4){
                CheckToMakeBombs();
            }
			findMatches.currentMatches.Clear();
        for (int i = 0; i < width; i ++){
            for (int j = 0; j < height; j++){
                if (allDots[i, j] != null){
                    
                    DestroyMatchesAt(i, j);
                }
            }
        }
        
        StartCoroutine(DecreaseRowCo2());
    }

	public void DamageConcrete(int column, int row)
	{
		if (column > 0)
		{
			if (concreteTiles[column - 1, row])
			{
				concreteTiles[column - 1, row].TakeDamage(1);
				if (concreteTiles[column - 1, row].hitPoints <= 0)
				{
					concreteTiles[column - 1, row] = null;
				}
			}
		}
		if (column < width - 1)
		{
			if (concreteTiles[column + 1, row])
			{
				concreteTiles[column + 1, row].TakeDamage(1);
				if (concreteTiles[column + 1, row].hitPoints <= 0)
				{
					concreteTiles[column + 1, row] = null;
				}
			}
		}
		if (row > 0)
		{
			if (concreteTiles[column, row - 1])
			{
				concreteTiles[column, row - 1].TakeDamage(1);
				if (concreteTiles[column, row - 1].hitPoints <= 0)
				{
					concreteTiles[column, row - 1] = null;
				}
			}
		}
		if (row < height - 1)
		{
			if (concreteTiles[column, row + 1])
			{
				concreteTiles[column, row + 1].TakeDamage(1);
				if (concreteTiles[column, row + 1].hitPoints <= 0)
				{
					concreteTiles[column, row + 1] = null;
				}
			}
		}
	}

	private IEnumerator DecreaseRowCo2()
	{
		for (int i = 0; i < width; i ++)
		{
			for (int j = 0; j < height; j ++)
			{
				//if the current spot isn't blank and is empty. . . 
				if(!blankSpaces[i,j] && allDots[i,j] == null )
				{
					//loop from the space above to the top of the column
					for (int k = j + 1; k < height; k ++)
					{
						//if a dot is found. . .
						if(allDots[i, k]!= null)
						{
							//move that dot to this empty space
							allDots[i, k].GetComponent<Dot>().row = j;
							//set that spot to be null
							allDots[i, k] = null;
							//break out of the loop;
							break;
						}
					}
				}
			}
		}

		yield return new WaitForSeconds(refillDelay * 0.5f);
		StartCoroutine(FillBoardCo());
	}

    private IEnumerator DecreaseRowCo(){
        int nullCount = 0;
        for (int i = 0; i < width; i ++){
            for (int j = 0; j < height; j ++){
                if(allDots[i, j] == null){
                    nullCount++;
                }else if(nullCount > 0){
                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
		yield return new WaitForSeconds(refillDelay * 0.5f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard(){
        for (int i = 0; i < width; i ++){
            for (int j = 0; j < height; j ++){
				if(allDots[i, j] == null && !blankSpaces[i,j] ){
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int dotToUse = Random.Range(0, dots.Length);
					int maxIterations = 0;

					while(MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100)
					{
						maxIterations++;
						dotToUse = Random.Range(0, dots.Length);
					}

					maxIterations = 0;
					GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i, j] = piece;
                    piece.GetComponent<Dot>().row = j;
                    piece.GetComponent<Dot>().column = i;

                }
            }
        }
		if (endGameManager.moves) { 
		if (endGameManager.currentvalue > 0)
		{
			endGameManager.DecreaseCount();
				endGameManager.moves = false;
		}
		}

		if (endGameManager.currentvalue <= 0 && scoreManager.score != scoreGoals)
		{
			handle = true;
		}


	}

    private bool MatchesOnBoard(){
        for (int i = 0; i < width; i ++){
            for (int j = 0; j < height; j ++){
                if(allDots[i, j]!= null){
                    if(allDots[i, j].GetComponent<Dot>().isMatched){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo(){
        
		yield return new WaitForSeconds(refillDelay);
		RefillBoard();
         yield return new WaitForSeconds(refillDelay);
        while(MatchesOnBoard()){

			streakValue ++;
			DestroyMatches();
			yield break;
			//yield return new WaitForSeconds(2 * refillDelay);
            
        }
        //findMatches.currentMatches.Clear();
        currentDot = null;
        

		if(IsDeadlocked())
		{
			StartCoroutine(ShuffleBoard());
			Debug.Log("Deadlocked!!!");
		}
		yield return new WaitForSeconds(refillDelay);
        currentState = GameState.move;
		streakValue = 1;

    }

	private void SwitchPieces(int column, int row, Vector2 direction)
	{
		//Take the second piece and save it in a holder
		GameObject holder = allDots[column + (int)direction.x, row + (int)direction.y] as GameObject;
        //switching the first dot to be the second position
		allDots[column + (int)direction.x, row + (int)direction.y] = allDots[column, row];
		//Set the first dot to be the second dot
		allDots[column, row] = holder;
	}

    private bool CheckForMatches()
	{
		for (int i = 0; i < width; i ++)
		{
			for (int j = 0; j < height; j ++)
			{
				if(allDots[i,j]!= null)
				{
					//Make sure that one and two to the right are in the
					//board
					if (i < width - 2)
					{
						//Check if the dots to the right and two to the right exist
						if (allDots[i + 1, j] != null && allDots[i + 2, j] != null)
						{
							if (allDots[i + 1, j].tag == allDots[i, j].tag
							   && allDots[i + 2, j].tag == allDots[i, j].tag)
							{
								return true;
							}
						}

					}
					if (j < height - 2)
					{
						//Check if the dots above exist
						if (allDots[i, j + 1] != null && allDots[i, j + 2] != null)
						{
							if (allDots[i, j + 1].tag == allDots[i, j].tag
							   && allDots[i, j + 2].tag == allDots[i, j].tag)
							{
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}

	public bool SwitchAndCheck(int column, int row, Vector2 direction)
	{
		SwitchPieces(column, row, direction);
		if(CheckForMatches())
		{
			SwitchPieces(column, row, direction);
			return true;
		}
		SwitchPieces(column, row, direction);
		return false;
	}

    private bool IsDeadlocked()
	{
		for (int i = 0; i < width; i ++)
		{
			for (int j = 0; j < height; j ++)
			{
				if(allDots[i,j]!= null)
				{
					if(i < width - 1)
					{
						if(SwitchAndCheck(i, j, Vector2.right))
						{
							return false;
						}
					}
					if(j < height - 1)
					{
						if(SwitchAndCheck(i,j,Vector2.up))
						{
							return false;
						}
					}
				}
			}
		}
		return true;
	}

	private IEnumerator ShuffleBoard()
	{
		yield return new WaitForSeconds(0.5f);
		//Create a list of game objects
		List<GameObject> newBoard = new List<GameObject>();
		//Add every piece to this list
		for (int i = 0; i < width; i ++)
		{
			for (int j = 0; j < height; j ++)
			{
				if(allDots[i,j]!= null)
				{
					newBoard.Add(allDots[i, j]);
				}
			}
		}
		yield return new WaitForSeconds(0.5f);
		//for every spot on the board. . . 
		for (int i = 0; i < width; i ++)
		{
			for (int j = 0; j < height; j ++)
			{
				//if this spot shouldn't be blank
				if(!blankSpaces[i,j])
				{
					//Pick a random number
					int pieceToUse = Random.Range(0, newBoard.Count);
                    
                    //Assign the column to the piece
					int maxIterations = 0;

					while (MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations < 100)
                    {
						pieceToUse = Random.Range(0, newBoard.Count);
                        maxIterations++;
                        Debug.Log(maxIterations);
                    }
					//Make a container for the piece
                    Dot piece = newBoard[pieceToUse].GetComponent<Dot>();
                    maxIterations = 0;
					piece.column = i;
                    //Assign the row to the piece
					piece.row = j;
                    //Fill in the dots array with this new piece
					allDots[i, j] = newBoard[pieceToUse];
                    //Remove it from the list
					newBoard.Remove(newBoard[pieceToUse]);
				}
			}
		}
        //Check if it's still deadlocked
		if(IsDeadlocked())
		{
			StartCoroutine(ShuffleBoard());
		}
	}

}
