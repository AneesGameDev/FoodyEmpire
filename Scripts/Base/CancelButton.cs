using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CancelButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
 public void backToPanel(){
       SceneManager.LoadScene("2ndScreen");
   }
    // Update is called once per frame
    void Update()
    {
        
    }
}
