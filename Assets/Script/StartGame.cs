using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
using GoogleMobileAds.Api;

public class StartGame : MonoBehaviour
{
    public RectTransform Title_;
    public RectTransform Button_;

    public GameObject Play;
    public GameObject VFX;
    public GameObject Company;

    public bool Flag = true;

    public float speed;
    public RectTransform Reference;
    

    public GameObject Highscore;
    public UnityEngine.UI.Button resume;

    // Start is called before the first frame update
    void Start()
    {

        //MobileAds.
        if(!Enviroment.AdInitialise){
         MobileAds.Initialize(initStatus => { Enviroment.AdInitialise=true;});

        }

        resume.interactable = false;
        Enviroment.LoadGame();
        if (Enviroment.CurrentLevel > 1)
        {
            resume.interactable = true;
        }

        //Title_ = Title.GetComponent<RectTransform>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Flag)
        {
            if (Title_.position.y > Reference.position.y)
            {

                Vector3 aPos = Title_.position;
                aPos.x = 0;
                aPos.y = speed * Time.deltaTime;
                Title_.position -= aPos;
               // Debug.Log("Menu no");

            }
            else
            {
               // Company.SetActive(true);
                //this.gameObject.GetComponent<AudioSource>().Play(0);
                StartCoroutine(Button());
//Debug.Log("Menu");
            }
        }

    }
    IEnumerator Button()
    {
        Flag = false;
        yield return new WaitForSeconds(0.7f);
        Play.SetActive(true);
        VFX.SetActive(true);

        // Cloud.OnInitializeComplete+=CloudOnceInitializeComplete;
        // Cloud.Initialize(false,true);
    }
    public void score()
    {



        // 

    }
    public void CloudOnceInitializeComplete()
    {
        Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
    }
    public void SubmitScore()
    {
        //Leaderboards.HIGHSCORES.SubmitScore((long)PlayerPrefs.GetInt("HighHike"));
        //Leaderboards.LEVELHIKE.SubmitScore((long)PlayerPrefs.GetInt("Level"));


    }

}
