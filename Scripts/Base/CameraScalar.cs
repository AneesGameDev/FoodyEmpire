using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalar : MonoBehaviour {

    private Board board;
    public float cameraOffset;
    public float aspectRatio ;
    public float padding ;
	public float yOffset ;

	// Use this for initialization
/*	private void Awake()
	{
        DontDestroyOnLoad(this.gameObject);
	}*/
	void Start () {
        board = FindObjectOfType<Board>();
        if(board!= null){
            RepositionCamera(board.width - 1, board.height - 1);
        }
	}
    private void Update()
    {
        RepositionCamera(board.width - 1, board.height - 1);
    }
    void RepositionCamera(float x, float y){
		Vector3 tempPosition = new Vector3(x/2, y/2 + yOffset, cameraOffset);
        transform.position = tempPosition;
        if (board.width >= board.height)
        {
            Camera.main.orthographicSize = (board.width / 2 + padding) / aspectRatio;
        }else{
            Camera.main.orthographicSize = board.height / 2 + padding;
        }
    }
	


}
