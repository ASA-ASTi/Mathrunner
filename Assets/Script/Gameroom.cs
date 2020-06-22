using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class Gameroom : MonoBehaviour
{
    public Text SliderText;
    public Text Code;
    public Slider slider,sliderTimer;
    public RawImage[] image;
    public Text[] DisplayName;
    public GameObject[] UI_;
    public GameObject ButtonHost;
    private FirebaseDatabase database;
    private DatabaseReference reference;
    ArrayList Players;
    public InputField input;
    public Text JoinInfo;
    public int indexprofile = 1;
    public int TotalNoPlayer = 0;
        public int Waiting = 0;


    // Start is called before the first frame update
    void Start()
    {

        image[0].texture = Enviroment.profilepicture;
        DisplayName[0].text = Enviroment.Name;


        // Set up the Editor before calling into the realtime database.
        // Set up the Editor before calling into the realtime database.
        database = FirebaseDatabase.DefaultInstance;



    }

    // Update is called once per frame
    void Update()
    {

    }
    public void restart(){

    }

    //Taking Slider Value
    public void Value_Slider()
    {
        SliderText.text = "Choose Players :" + " " + slider.value;
    }
    public void Input_Text()
    {
        Code.text = input.text;


    }
    public void HUL(){
         UI_[0].SetActive(true);
            UI_[1].SetActive(false);
            UI_[2].SetActive(false);
    }
    private void save(int i, string name, string Username, String picUrl)
    {
        PlayerPrefs.SetString("NameP" + i, name);
        PlayerPrefs.SetString("UsernameP" + i, name);
        PlayerPrefs.SetString("UrlPicP" + i, name);

    }

    //Creating Room
    public void Createroom()
    {

        //UI Work    
        UI_[0].SetActive(false);
        UI_[1].SetActive(true);
        database.GetReference("Users/Gamerooms/" + Enviroment.Username + "/Multiplayer").RemoveValueAsync();

        Code.text = Enviroment.Username;
        Enviroment.HostName = Enviroment.Username;
        Enviroment.multiplayerId = 1;

        //Host Setting
        Game_details game = new Game_details();
        game.No_Players = (int)slider.value;
        game.time=(int)sliderTimer.value;
        TotalNoPlayer = game.No_Players;

        string json2 = JsonUtility.ToJson(game);
        database.GetReference("Users/Gamerooms/" + Enviroment.Username + "/Multiplayer/Game_details").SetRawJsonValueAsync(json2);

        Player player = new Player();

        player.Playername = Enviroment.Name;
        player.playerId = 1;
        player.Username = Enviroment.Username;
        player.uri = Enviroment.uri;
        string json = JsonUtility.ToJson(player);


        database.GetReference("Users/Gamerooms/" + Enviroment.Username + "/Multiplayer/Players/" + Enviroment.Username).SetRawJsonValueAsync(json);
        //  reference = database.GetReference("Users/Gamerooms/" + Enviroment.Username + "/Multiplayer/Players");
        // reference.ValueChanged += Fetching_Player_JOIN;
        reference = database.GetReference("Users/Gamerooms/" + Code.text + "/Multiplayer/Players");
        reference.ValueChanged += Fetching_Player;
        ButtonHost.SetActive(true);
        PlayerPrefs.SetInt("Host", 1);
        PlayerPrefs.SetInt("noOfPlayers", TotalNoPlayer);

    }

    private void Fetching_Player(object sender, ValueChangedEventArgs e)
    {
        indexprofile = 1;
        Waiting=0;

        /*indexprofile = 2;
        for (int i = 1; i  <= TotalNoPlayer; i++)
        {
            var json = e.Snapshot.Child("" + (i + 1)).GetRawJsonValue();
            if (!string.IsNullOrEmpty(json))
            {
                var player = JsonUtility.FromJson<Player>(json);
                StartCoroutine(setPlayerInfo(player.uri, indexprofile, player.Playername));
                
            }
        }*/
        foreach (var data_ in e.Snapshot.Children)
        {
            var json = data_.GetRawJsonValue();
            if (!string.IsNullOrEmpty(json))
            {
                var player = JsonUtility.FromJson<Player>(json);
                if (player.Username != Enviroment.Username)
                {
                    StartCoroutine(setPlayerInfo(player.uri, indexprofile, player.Playername));
                    save(indexprofile, player.Playername, player.Username, player.uri);
                    PlayerPrefs.SetString("purl"+indexprofile,player.uri);
                    indexprofile++;
                   
                }
                 Waiting++;

            }
        }



    }
    private void Fetching_Player_JOIN(object sender, ValueChangedEventArgs e)
    {
        indexprofile = 1;
        Waiting=0;
        /* for (int i = 1; i < 6; i++)
         {

             if (Enviroment.multiplayerId != i)
             {


                 var json = e.Snapshot.Child("" + (i)).GetRawJsonValue();
                 if (!string.IsNullOrEmpty(json))
                 {
                     var player = JsonUtility.FromJson<Player>(json);
                     StartCoroutine(setPlayerInfo(player.uri, i, player.Playername));
                 }
             }
         }

         */
        foreach (var data_ in e.Snapshot.Children)
        {
            var json = data_.GetRawJsonValue();
            if (!string.IsNullOrEmpty(json))
            {
                var player = JsonUtility.FromJson<Player>(json);
                if (!player.Username.Equals(Enviroment.Username))
                {
                    StartCoroutine(setPlayerInfo(player.uri, indexprofile, player.Playername));
                    save(indexprofile, player.Playername, player.Username, player.uri);

                    indexprofile++;
                }
                 Waiting++;

            }
        }
    }

    public void StartGame_Host()
    {
        if(Waiting==TotalNoPlayer){
database.GetReference("Users/Gamerooms/" + Enviroment.Username + "/Multiplayer/Game_details/Gamestart").SetValueAsync(1);
        //
        //database.GetReference("Users/Gamerooms/"+Enviroment.Username+"/Multiplayer/Game_details/p1").SetValueAsync(1);
        Enviroment.LoadLevel = 6;
        SceneManager.LoadScene(0);
        }
        

    }
    public void StartGame_Join()
    {

        database.GetReference("Users/Gamerooms/" + input.text + "/Multiplayer/Game_details")
       .GetValueAsync().ContinueWith(task =>
       {
           if (task.IsFaulted)
           {
              // Handle the error...

          }
           else if (task.IsCompleted)
           {
               DataSnapshot snapshot = task.Result;
               if (snapshot.Exists)
               {
                   Game_details temp = new Game_details();
                   var json = snapshot.GetRawJsonValue();
                   temp = JsonUtility.FromJson<Game_details>(json);
                   if (temp.Gamestart == 1)
                   {
                       Enviroment.LoadLevel = 6;
                       SceneManager.LoadScene(0);
                   }
                  //JoinInfo.text = "Done!";

              }
               else
               {
                  //JoinInfo.text = "No Game Found!";
              }
              // Do something with snapshot...
          }
       });
    }


    private void OnDestroy()
    {
        reference.ValueChanged -= Fetching_Player;
        reference.ValueChanged -= Fetching_Player_JOIN;
        reference = null;
        database = null;
    }

    public void JoinGame()
    {

        if (!input.text.Equals(""))
        {




            UI_[0].SetActive(false);
            UI_[1].SetActive(false);
            UI_[2].SetActive(true);
            GameExist_();



            /*
          // GameInfo_Online game = new GameInfo_Online();
         // game=LoadGameInfo();
            Player player=new Player();

          player.Playername=Enviroment.Name;
          player.playerId=2;
          player.uri=Enviroment.uri;
          string json = JsonUtility.ToJson(player);
         // string json = JsonUtility.ToJson(game);
          // if(!LoadPlayer_Host().Equals(null)){
              var strt=GameExist();
              strt.Wait();
              if(strt.Result){

                  var roomfull=GameStart_();
                  roomfull.Wait();
                  if(roomfull.Result==0){



                  var count_=PlayerCount();
                  count_.Wait();
                  if(count_.Result<5){
                   database.GetReference("Users/Gamerooms/"+Code.text+"/Multiplayer/Players/"+(count_.Result+1)).SetRawJsonValueAsync(json);
                   reference=database.GetReference("Users/Gamerooms/"+Code.text+"/Multiplayer/Players");
                   reference.ValueChanged+=HandleValueChanged;
                  }
                  else{
                     Code.text="Room Full"; 
                  }
                  }
                  else{
                      Code.text="Game Started";
                  }


              }
              else{
                  Code.text="No Such Game Found";
              }

              // Player Temp = new Player();
              // Temp = LoadPlayer_Host();
               //StartCoroutine(LoadPlayer_PersonalInfo(Temp.uri,2,Temp.Playername));
          // }
          */

        }
        else
        {
            input.text = "Please Enter Code";
        }
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        var json = e.Snapshot.Child("1").GetRawJsonValue();
        if (!string.IsNullOrEmpty(json))
        {
            var player = JsonUtility.FromJson<Player>(json);
            StartCoroutine(setPlayerInfo(player.uri, 2, player.Playername));
        }
    }
    private void GameExist_()
    {
        JoinInfo.text = "Fetching Game Info!";
        database.GetReference("Users/Gamerooms/" + input.text + "/Multiplayer/Game_details")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              // Handle the error...

          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              if (snapshot.Exists)
              {
                  Game_details temp = new Game_details();
                  var json = snapshot.GetRawJsonValue();
                  temp = JsonUtility.FromJson<Game_details>(json);
                  TotalNoPlayer = temp.No_Players;
                  JoinInfo.text = "Done!";
                  PlayerPrefs.SetInt("TotalPlayer", TotalNoPlayer);
                  // PlayerPrefs.SetString("HostName",input.text);
                  Enviroment.HostName = input.text;
                  PlayerPrefs.SetInt("noOfPlayers", TotalNoPlayer);

                  PlayerCount(TotalNoPlayer);

              }
              else
              {
                  JoinInfo.text = "No Game Found!";
              }
              // Do something with snapshot...
          }
      });

    }

    public async Task<bool> GameExist()
    {

        var datasnapshot = await database.GetReference("Users/Gamerooms/" + input.text + "/Multiplayer/c").GetValueAsync();
        return datasnapshot.Exists;
    }

    public void PlayerCount(int count)
    {
        JoinInfo.text = "Fetching PlayerInfo Info!";
        database.GetReference("Users/Gamerooms/" + input.text + "/Multiplayer/Players")
           .GetValueAsync().ContinueWith(task =>
           {
               if (task.IsFaulted)
               {
                   // Handle the error...

               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   if (snapshot.ChildrenCount <= count)
                   {
                       JoinInfo.text = "Joining In!";
                       Player player = new Player();
                       Enviroment.multiplayerId = snapshot.ChildrenCount + 1;
                       player.Playername = Enviroment.Name;
                       player.Username = Enviroment.Username;
                       player.playerId = snapshot.ChildrenCount + 1;
                       player.uri = Enviroment.uri;
                       Enviroment.multiplayerId = player.playerId;
                       string json = JsonUtility.ToJson(player);
                       setme_join(Enviroment.Username, json);
                   }
                   else
                   {
                       JoinInfo.text = "Game Room full!";
                   }
                   // Do something with snapshot...
               }
           });

    }

    private void setme_join(string name, string json)
    {
        database.GetReference("Users/Gamerooms/" + input.text + "/Multiplayer/Players/" + name).SetRawJsonValueAsync(json);
        reference = database.GetReference("Users/Gamerooms/" + input.text + "/Multiplayer/Players");
        reference.ValueChanged += Fetching_Player_JOIN;
        UI_[1].SetActive(true);
        UI_[2].SetActive(false);
        InvokeRepeating("StartGame_Join", 1f, 1.5f);

    }


    public async Task<long> GameStart_()
    {

        var datasnapshot = await database.GetReference("Users/Gamerooms/" + Code.text + "/Multiplayer/Game_details/Gamestart").GetValueAsync();
        return (long)datasnapshot.Value;
    }
    private Player LoadPlayer_Host()
    {
        var datasnapshot = database.GetReference("Users/Gamerooms/" + input.text + "/Players/1").GetValueAsync();
        if (datasnapshot.Equals(null))
        {
            return null;
        }
        return JsonUtility.FromJson<Player>(datasnapshot.Result.GetRawJsonValue());
    }
    private Game_details LoadGameInfo()
    {
        var datasnapshot = database.GetReference("Users/Gamerooms/" + Enviroment.Username + "/GameInformation").GetValueAsync();

        return JsonUtility.FromJson<Game_details>(datasnapshot.Result.GetRawJsonValue());
    }


    private IEnumerator setPlayerInfo(String url, int Player, String name)
    {

        Uri myUri = new Uri(url, UriKind.Absolute);
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(myUri))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
            }
            else
            {
                Texture2D tex = new Texture2D(2, 2);
                tex = DownloadHandlerTexture.GetContent(uwr);

                image[Player].texture = tex;
                DisplayName[Player].text = name;
            }
        }



    }

    void setDataJoin(int i, string uri, string name)
    {
        DisplayName[i].text = Enviroment.Name;
        Uri myUri = new Uri(uri, UriKind.Absolute);
        StartCoroutine(setImage(myUri, i));
        SceneManager.LoadSceneAsync("Multiplayer_InGame");
    }
    IEnumerator setImage(Uri url, int i)
    {
        // AddToInformation("pic");
        Enviroment.uri = url + "";
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                //AddToInformation("error");

            }
            else
            {
                Texture2D tex = new Texture2D(2, 2);
                tex = DownloadHandlerTexture.GetContent(uwr);
                image[i].texture = tex;

            }
        }


        SceneManager.LoadScene(6);
    }
}
