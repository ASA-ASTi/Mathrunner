using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
   public GameObject Player;
   public GameObject Computer;
      public GameObject tutorial_;


    // Start is called before the first frame update

     private void Awake() {
       Enviroment.WalkP=false; 
       Enviroment.WalkC=false;
       Player.GetComponent<Animator>().enabled=false;
       Computer.GetComponent<Animator>().enabled=false;
    }
    void Start()
    {
        StartCoroutine(Sequence());
    }

  
    IEnumerator Sequence( ){
         yield return new WaitForSeconds(0.5f);
        Player.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        Enviroment.WalkP=true; 
        Player.GetComponent<Animator>().enabled=true;
        yield return new WaitForSeconds(3.0f);
        Computer.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        Enviroment.WalkC=true; 
        Computer.GetComponent<Animator>().enabled=true;
        yield return new WaitForSeconds(5.0f);




        tutorial_.SetActive(true);
        // Debug.Log ("Waited a sec");
     }
     public void load(){
         //PlayerPrefs.SetInt("tutorial",1);
         Enviroment.LoadLevel=9;
         UnityEngine.SceneManagement.SceneManager.LoadScene(0);

     }
   
}
