using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundScript : MonoBehaviour {

    public AudioSource bgSound;
    // Use this for initialization
    void Start () {

        bgSound = GameObject.FindWithTag("BGSound").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
               // soundButton.sprite = musicOffSprite;
                bgSound.Pause();

            }
            else
            {
                //soundButton.sprite = musicOnSprite;
                // PlayerPrefs.SetInt("Sound", 0);
                bgSound.Play();
            }
        }
        else
        {
            //soundButton.sprite = musicOnSprite;
            //PlayerPrefs.SetInt("Sound", 1);
            bgSound.Play();
        }
    }

    //Play Global
    private static BGSoundScript instance = null;
    public static BGSoundScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    //Play Gobal End

    // Update is called once per frame
    void Update () {
		
	}
}
