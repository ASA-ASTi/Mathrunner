using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_Game : MonoBehaviour
{
    
    public Slider Progressbar;
     void Start()
    {
        StartCoroutine(LoadScene());
    }
        IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Enviroment.LoadLevel);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
       
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress

            Progressbar.value=asyncOperation.progress * 100;

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
               
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
         
}
