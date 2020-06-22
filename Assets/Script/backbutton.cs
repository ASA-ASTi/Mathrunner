using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backbutton : MonoBehaviour
{
    
    void Update()
    {
       if(Input.GetKeyUp(KeyCode.Escape)){
        //initialte_PauseMenu();
        //Application.Quit();
        Enviroment.LoadLevel=1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }  
    }

    public void OpenInsta(){
        Application.OpenURL("https://www.instagram.com/asaasti_production/");
    }
}
