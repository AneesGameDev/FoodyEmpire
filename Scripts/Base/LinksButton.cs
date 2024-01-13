using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksButton : MonoBehaviour
{
    public GameObject threeDots;
    // Start is called before the first frame update
    void Start()
    {
		if (threeDots != null)
		{
            threeDots.SetActive(false);
		}
    }
    public void threeDotsOn()
	{
        threeDots.SetActive(true);
    }
    public void threeDotsOf()
    {
        threeDots.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OurApps()
	{
        Application.OpenURL("http://oakmicrosystem.ml/login.php");
	}
    public void OurGames()
	{
        Application.OpenURL("http://oakmicrosystem.ml/login.php");
	}
    public void feedBack()
	{
        Application.OpenURL("mailto:muhammadanees7765@gmail.com");
	}
    public void AboutUs()
	{
        Application.OpenURL("http://oakmicrosystem.ml/login.php");
	}
    public void ShareApp()
	{

	}
}
