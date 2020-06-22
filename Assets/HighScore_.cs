using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore_ : MonoBehaviour
{
    public UnityEngine.UI.Text[] level;
    // Start is called before the first frame update
    void Start()
    {


        CloudOnce.Cloud.OnInitializeComplete += CloudOnceInitializeComplete;
        CloudOnce.Cloud.Initialize(false, true);
        //Enviroment.LoadGame();
        //l/evel.text = "" + Enviroment.CurrentLevel;

        level[0].text=""+PlayerPrefs.GetInt("LevelMix");
        level[1].text=""+PlayerPrefs.GetInt("LevelAdd");
        level[2].text=""+PlayerPrefs.GetInt("LevelSub");
        level[3].text=""+PlayerPrefs.GetInt("LevelMul");
        level[4].text=""+PlayerPrefs.GetInt("LevelDiv");

    }
    public void CloudOnceInitializeComplete()
    {
        SubmitScore();
        CloudOnce.Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
    }
    public void SubmitScore(){
       //Leaderboards.HIGHSCORES.SubmitScore((long)PlayerPrefs.GetInt("HighHike"));
       CloudOnce.Leaderboards.Mixed.SubmitScore((long)PlayerPrefs.GetInt("LevelMix"));
        CloudOnce.Leaderboards.Addition.SubmitScore((long)PlayerPrefs.GetInt("LevelAdd"));
       CloudOnce.Leaderboards.Subtraction.SubmitScore((long)PlayerPrefs.GetInt("LevelSub"));
       CloudOnce.Leaderboards.Multiplication.SubmitScore((long)PlayerPrefs.GetInt("LevelMul"));
       CloudOnce.Leaderboards.Division.SubmitScore((long)PlayerPrefs.GetInt("LevelDiv"));

       

    }


}
