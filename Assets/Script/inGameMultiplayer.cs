using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class inGameMultiplayer : MonoBehaviour
{
    public Text StaminaBoard;
    public Text DistanceBoard;
    public Text GameOverBoard;
    public GameObject[] OBJ;
    public GameObject Menu_;
    public int NumberOfPlayer;
    public Human_mul2[] Players;
    public Human_mul PlayerLocal;
    public Transform OnlinePlayersSkelaton;
    private DatabaseReference fetching_Player, fetching_State;
    private FirebaseDatabase database;
    private location Locationtemp;
    public GameObject WinnerUI, QuestionUI;
    public int gameoverpos;
    public int gameoverstates;
    public bool win = false;
    public Text[] Scorecard;
    public Text[] Results;
    List<scoreman> listOfUsers;
    QuestionAnswers abc;
    private float Time_Temp = 60f;
    private int Time_;
    private Text TimerDisplay;
    private bool _Helper;
    List<scoreman> Score_Card;
    private bool gameOver=true;



    // Start is called before the first frame update
    void Start()
    {
        //Enviroment.Level_=1;
        //Enviroment.CurrentLevel=1;
        Score_Card = new List<scoreman>();
        NumberOfPlayer = PlayerPrefs.GetInt("noOfPlayers");
        Time_ = PlayerPrefs.GetInt("TimeOut");
        Enviroment.setStamina = 30;
        listOfUsers = new List<scoreman>();

        //Spawning Other Players
        Players = new Human_mul2[NumberOfPlayer - 1];
        for (int i = 0; i < NumberOfPlayer - 1; i++)
        {
            var gameObj = Instantiate(OnlinePlayersSkelaton, new Vector3(-21.11f, 3.45f, 0f), transform.rotation);
            Players[i] = gameObj.GetComponent<Human_mul2>();
        }
        Enviroment.setDistance = 0f;
        database = FirebaseDatabase.DefaultInstance;

        Locationtemp = new location();
        //InvokeRepeating("fetching_ReadyState",0f,0.5f);
        if (Enviroment.multiplayerId == 1)
        {
            database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/Game_details/p1").SetValueAsync(1);

        }
        else if (Enviroment.multiplayerId == 2)
        {
            database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/Game_details/p2").SetValueAsync(1);

        }
        else if (Enviroment.multiplayerId == 3)
        {
            database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/Game_details/p2").SetValueAsync(1);

        }
        else if (Enviroment.multiplayerId == 4)
        {
            database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/Game_details/p2").SetValueAsync(1);

        }
        else if (Enviroment.multiplayerId == 5)
        {
            database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/Game_details/p2").SetValueAsync(1);

        }
        fetching_State = database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/Game_details");
        fetching_State.ValueChanged += fetching_ReadyState;
        Enviroment.Question_Generation = false;
        abc.NewQuestion = false;
    }

    private void TimeCalculation()
    {
        Time_Temp = Time_Temp - Time.deltaTime;
        if (Time_Temp <= 0)
        {
            Time_ = Time_ - 1;
            Time_Temp = 60f;
            //Time_Temp=60f
        }

        if (Time_ <= 0)
        {
            PlayerLocal.TimeOut = false;
        }

        if (_Helper)
        {
            _Helper = false;
            TimerDisplay.text = "" + Time_ + ":" + Time_Temp;
        }
        else
        {
            _Helper = true;
            TimerDisplay.text = "" + Time_ + " " + Time_Temp;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Enviroment.setStamina <= 0)
        {
            //OBJ[2].GetComponent<Animator>().enabled = false;
            //OBJ[2].GetComponent<Human_mul>().Start_Game = false;
            //OBJ[2].GetComponent<Human_mul>().UploadData(0,true);
            PlayerLocal.Start_Game = false;
            //PlayerLocal.UploadData(0,false);
            // OBJ[0].SetActive(false);
            // OBJ[1].SetActive(true);
            //GameOverBoard.text = "GAME OVER !\n You Were="+gameoverpos; //+ Enviroment.Retry;

        }
        else if (PlayerLocal.TimeOut && Enviroment.setStamina >= 1)
        {
            Enviroment.setStamina = Enviroment.setStamina - Time.deltaTime;
            Enviroment.TimerSet = Enviroment.TimerSet + Time.deltaTime;
            PlayerLocal.Start_Game = true;

        }
        if(!PlayerLocal.TimeOut){
            //Do Something to Calculate
            if(gameOver){
                gameOver=false;
            }
        }


        PaintScreen();


    }
    private void PaintScreen()
    {

        StaminaBoard.text = "" + (int)Enviroment.setStamina;
        DistanceBoard.text = "" + (int)Enviroment.setDistance + "m";

    }
    public void Restart()
    {

        Enviroment.DecrementRetry();
        Enviroment.setStamina = Enviroment.MaxStamina / 3;
        Enviroment.setGameOver = false;
        Enviroment.WalkP = true;
        OBJ[2].GetComponent<Animator>().enabled = true;
        OBJ[0].SetActive(true);
        OBJ[1].SetActive(false);
        //Enviroment.SaveGame();


    }
    public void Exit()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(y);
        Enviroment.LoadLevel = 1;
        SceneManager.LoadScene(0);
        // Enviroment.SaveGame();
        // SubmitScore();
        // SubmitScore();
    }
    private void Retry()
    {
        Enviroment.SaveGame();
        //SubmitScore();
    }
    public void SubmitScore()
    {
        // Leaderboards.HIGHSCORES.SubmitScore((long)Enviroment.setDistance);
    }
    private void fetching_ReadyState(object sender, ValueChangedEventArgs e)
    {

        var json = e.Snapshot.GetRawJsonValue();
        if (!string.IsNullOrEmpty(json))
        {
            var player = JsonUtility.FromJson<Game_details>(json);
            switch (player.No_Players)
            {
                case 2:
                    if (player.p1 == 1 && player.p2 == 1)
                    {

                        //Starting Player
                        Players[0].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        fetching_Names(1);
                        set_newEnviroment();

                    }
                    break;
                case 3:
                    if (player.p1 == 1 && player.p2 == 1 && player.p3 == 1)
                    {
                        //Start_Game = true;
                        Players[0].Start_Game = true;
                        Players[1].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        fetching_Names(2);


                        set_newEnviroment();

                    }
                    break;
                case 4:
                    if (player.p1 == 1 && player.p2 == 1 && player.p3 == 1 && player.p4 == 1)
                    {
                        Players[0].Start_Game = true;
                        Players[1].Start_Game = true;
                        Players[2].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        fetching_Names(3);


                        set_newEnviroment();


                    }
                    break;
                case 5:
                    if (player.p1 == 1 && player.p2 == 1 && player.p3 == 1 && player.p4 == 1 && player.p5 == 1)
                    {
                        Players[0].Start_Game = true;
                        Players[1].Start_Game = true;
                        Players[2].Start_Game = true;
                        Players[3].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        fetching_Names(4);


                        set_newEnviroment();


                    }
                    break;
            }
        }

    }

    private void fetching_GameOverStates(object sender, ValueChangedEventArgs e)
    {

        var json = e.Snapshot.GetRawJsonValue();
        if (!string.IsNullOrEmpty(json))
        {
            var player = JsonUtility.FromJson<Game_details>(json);
            switch (player.No_Players)
            {
                case 2:
                    if (player.p1 == 1 && player.p2 == 1)
                    {

                        //Starting Player
                        Players[0].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        fetching_Names(1);
                        set_newEnviroment();
                        PlayerLocal.TimeOut = true;
                        InvokeRepeating("Check_", 5f, 1f);
                        //InvokeRepeating("Scorecard_set", 2f, 0.5f);
                        Enviroment.Question_Generation = true;
                        abc.NewQuestion = true;

                    }
                    break;
                case 3:
                    if (player.p1 == 1 && player.p2 == 1 && player.p3 == 1)
                    {
                        //Start_Game = true;
                        Players[0].Start_Game = true;
                        Players[1].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        PlayerLocal.TimeOut = true;

                        fetching_Names(2);


                        set_newEnviroment(); InvokeRepeating("Check_", 5f, 1f);
                        // InvokeRepeating("Scorecard_set", 2f, 0.5f);
                        Enviroment.Question_Generation = true;
                        abc.NewQuestion = true;

                    }
                    break;
                case 4:
                    if (player.p1 == 1 && player.p2 == 1 && player.p3 == 1 && player.p4 == 1)
                    {
                        Players[0].Start_Game = true;
                        Players[1].Start_Game = true;
                        Players[2].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        PlayerLocal.TimeOut = true;

                        fetching_Names(3);


                        set_newEnviroment();
                        //InvokeRepeating("Scorecard_set", 2f, 0.5f);
                        InvokeRepeating("Check_", 5f, 1f);

                        Enviroment.Question_Generation = true;
                        abc.NewQuestion = true;
                    }
                    break;
                case 5:
                    if (player.p1 == 1 && player.p2 == 1 && player.p3 == 1 && player.p4 == 1 && player.p5 == 1)
                    {
                        Players[0].Start_Game = true;
                        Players[1].Start_Game = true;
                        Players[2].Start_Game = true;
                        Players[3].Start_Game = true;
                        PlayerLocal.Start_Game = true;
                        PlayerLocal.TimeOut = true;
                        fetching_Names(4);


                        set_newEnviroment();
                        InvokeRepeating("Check_", 5f, 1f);
                        Enviroment.Question_Generation = true;
                        abc.NewQuestion = true;

                    }
                    break;
            }
        }

    }
    private void set_newEnviroment()
    {
        fetching_State.ValueChanged -= fetching_ReadyState;
        fetching_Player = database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/PlayersLocation");
        fetching_Player.ValueChanged += fetching_PositionState;

    }
    private void OnDestroy()
    {
        fetching_Player.ValueChanged -= fetching_PositionState;
        // fetchingPlayer.ValueChanged -= fetching_ReadyState;
        fetching_Player = null;
        fetching_Player.ValueChanged -= fetching_PositionState;

        database = null;
    }

    private void fetching_Names(int no)
    {
        for (int i = 1; i <= no; i++)
        {
            Players[i - 1].setName(PlayerPrefs.GetString("NameP" + i));
        }
    }
    private void fetching_PositionState(object sender, ValueChangedEventArgs e)
    {
        int index = 0;
        /*
        for (int i = 1; i <= NumberOfPlayer; i++)
        {
            if (Enviroment.multiplayerId != i)
            {

                var json = e.Snapshot.Child("i").GetRawJsonValue();
                if (!string.IsNullOrEmpty(json))
                {
                    Locationtemp = JsonUtility.FromJson<location>(json);
                    Players[index].MoveSpeed=Locationtemp.movespeed;
                    Players[index].Start_Game=!Locationtemp.gameOver;

                    index++;

                }
            }
        }*/


        List<scoreman> Temp = new List<scoreman>();

        foreach (var data_ in e.Snapshot.Children)
        {


            string Name = data_.Key;
            if (Name != Enviroment.Username)
            {


                var json = data_.GetRawJsonValue();
                if (!string.IsNullOrEmpty(json))
                {
                    Locationtemp = JsonUtility.FromJson<location>(json);

                    // Players[index].MoveSpeed=Locationtemp.movespeed;
                    // Players[index].Start_Game=!Locationtemp.gameOver;
                    //Players[index].st
                    Players[index].SetData(Locationtemp.movespeed, Locationtemp.gameOver, Locationtemp.distance, Locationtemp.x);

                    index += 1;

                    Temp.Add(new scoreman(Locationtemp.distance, data_.Key));


                }
            }
        }
        // gameoverpos = NumberOfPlayer - index_;


        //if (index_ == NumberOfPlayer - 1)
        // {
        //Winner Code
        //  WinnerScreen();
        //    fetching_Player.ValueChanged -= fetching_PositionState;

        //  }
        Temp.Add(new scoreman(Enviroment.setDistance, Enviroment.Username));
        // Scorecard_set(Temp);
        if (SCLOCK)
        {
            StartCoroutine(Scorecard_set(Temp));
        }


    }
    private void WinnerScreen()
    {
        PlayerLocal.Start_Game = false;
        WinnerUI.SetActive(true);
        QuestionUI.SetActive(false);


    }

    public void LoadLevel(int i)
    {
        Enviroment.LoadLevel = i;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    private void Check_()
    {
        if (!PlayerLocal.TimeOut)
        {

            PlayerLocal.Start_Game = false;
            StartCoroutine(Result_F());
        }
    }
    IEnumerator Result_F()
    {

        yield return 2f;
        WinnerUI.SetActive(true);
        QuestionUI.SetActive(false);
        for (int i = 0; i < NumberOfPlayer; i++)
        {
            Results[i].text = i + " " + Score_Card[i].name + "=" + Score_Card[i].distance;
        }

    }
    private bool SCLOCK = true;
    IEnumerator Scorecard_set(List<scoreman> abc)
    {


        SCLOCK = false;
        List<scoreman> sortedUsers = abc.OrderByDescending(t => t.distance).ToList();
        for (int i = 0; i < NumberOfPlayer; i++)
        {
            Scorecard[i].text = (i + 1) + "" + sortedUsers[i].name;
        }

        Score_Card = sortedUsers;
        SCLOCK = true;
        // }
        yield return null;

    }



}
