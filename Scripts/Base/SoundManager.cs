using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
	public AudioSource[] destroyNoise;
public Board board ;
public void Start()
{
	board=FindObjectOfType<Board>();
}
    public void PlayRandomDestroyNoise()
	{
		if (PlayerPrefs.HasKey("Sound")) {
			if (PlayerPrefs.GetInt("Sound") == 1)
			{
		//Choose a random number
		int clipToPlay = Random.Range(0, destroyNoise.Length);
		//play that clip
		destroyNoise[clipToPlay].Play();
	}
		}
		else
		{
			int clipToPlay = Random.Range(0, destroyNoise.Length);
			//play that clip
			destroyNoise[clipToPlay].Play();
		}
	}
}
