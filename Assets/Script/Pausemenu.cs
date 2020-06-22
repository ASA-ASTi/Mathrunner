using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public GameObject Ui;
    public UnityEngine.UI.Text txt; 
    public QuestionAnswers questionAnswers;
    private void Start() {
        Time.timeScale=0;
    }
    private void Update() {
       
        if (Input.GetKeyDown(KeyCode.Escape)) {
                Time.timeScale=1;
        Destroy(this.gameObject);

        }
     
    }
    public void resume(){
        Time.timeScale=1;
        //..Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
    public void Exit(){
        Time.timeScale=1;
        Enviroment.LoadLevel=1;
        SceneManager.LoadScene(0);
    }
    public void ExitAPP(){
       Application.Quit();
    }
    public void Play(){
      Time.timeScale=1;
        Enviroment.LoadLevel=9;
        SceneManager.LoadScene(0);
    }
    public void Exit_mainmenu(){
        Time.timeScale=1;
       // Enviroment.LoadLevel=1;
        //SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
   public void difficulty(int i){

       PlayerPrefs.SetInt("isEasy",i);
        if(i==0){
             Enviroment.isEasy=true;
        }
        else{
              Enviroment.isEasy=false;
              questionAnswers.Randomize(questionAnswers.QuestionsBank);
        }
        this.gameObject.SetActive(false);
        Ui.SetActive(true);
        Time.timeScale=1;
        questionAnswers.NewQuestion=true;

   }
    
}
