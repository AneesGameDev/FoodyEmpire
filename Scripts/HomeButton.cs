using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeButton : MonoBehaviour
{
    //public GameObject homepanel;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void backhomebtn()
    {
        SceneManager.LoadScene("1stScene");
        
    }
   
}
