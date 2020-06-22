using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

using CloudOnce;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{
    public Text StaminaBoard;
    public Text DistanceBoard;
    public Text GameOverBoard;

    public GameObject[] OBJ;
    public RectTransform UI_Score,locTemp;
    public Button AdvertiseMent;
    public GameObject UIpausemenu;
    public GameObject Menu_;
    public Button contun;
    public AudioClip[] clip_;
    public AudioSource player_;
    private RewardedAd rewardedAd;
        private BannerView bannerView;
        readonly  private   string banner_s="ca-app-pub-1358520088420849/1303143772";
       readonly  private string RewardAd_s="ca-app-pub-1358520088420849/5759842651";





    // Start is called before the first frame update
    void Start()
    {

        Enviroment.WalkP = true;
        Enviroment.setDistance = 0f;
        Enviroment.setStamina=30;
        Enviroment.Retry = 3;


        this.bannerView = new BannerView(banner_s, AdSize.Banner, AdPosition.Top);


// Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;


        // Create an empty ad request.
        AdRequest request2 = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request2);
        CreateAndLoadRewardedAd();



    }
    private void OnDestroy() {
        bannerView.Destroy();

    }
     public void CreateAndLoadRewardedAd()
    {
      
          
       

        this.rewardedAd = new RewardedAd(RewardAd_s);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Enviroment.setStamina <= 0)
        {
            Enviroment.setGameOver = true;
            Enviroment.WalkP = false;
            OBJ[2].GetComponent<Animator>().enabled = false;
            OBJ[0].SetActive(false);
            OBJ[1].SetActive(true);

            if (player_.clip == clip_[0])
            {
                player_.clip = clip_[1];
                player_.loop = false;
                player_.Play();
            }


            if (Enviroment.Retry > 0)
            {
                GameOverBoard.text = "Game Over \nContinue? " + Enviroment.Retry;

            }
            else
            {
                GameOverBoard.text = "Game Over\n Math Champ";
                Enviroment.SaveGame();
                //SubmitScore();


            }


        }
        else
        {
            Enviroment.setStamina = Enviroment.setStamina - Time.deltaTime;
            Enviroment.TimerSet = Enviroment.TimerSet + Time.deltaTime;

        }

        PaintScreen();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            initialte_PauseMenu();
        }

    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //string type = args.Type;
        //double amount = args.Amount;
        Continue_();
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    private void PaintScreen()
    {

        StaminaBoard.text = "" + (int)Enviroment.setStamina;
        DistanceBoard.text = "" + (int)Enviroment.setDistance+"m";

    }
    public void Restart()
    {




        if (Enviroment.Retry >= 0)
        {
                            
this.rewardedAd.Show();
           /* if (this.rewardedAd.IsLoaded())
            {
                
            }
            else{
                 
            }
        }*/


    }
    }
    private void Continue_()
    {
        Enviroment.DecrementRetry();
        Enviroment.setStamina = Enviroment.MaxStamina / 3;
        Enviroment.setGameOver = false;
        Enviroment.WalkP = true;
        OBJ[2].GetComponent<Animator>().enabled = true;
        OBJ[0].SetActive(true);
        OBJ[1].SetActive(false);
        Enviroment.SaveGame();
        if (player_.clip == clip_[1])
        {
            player_.clip = clip_[0];
            player_.loop = true;
            player_.Play();
        }
    }
    public void Exit()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(y);
        Enviroment.LoadLevel = 1;
        SceneManager.LoadScene(0);
        Enviroment.SaveGame();
        // SubmitScore();
        // SubmitScore();
    }
    public void Retry()
    {
        Enviroment.SaveGame();
        Enviroment.LoadLevel = 2;
        SceneManager.LoadScene(0);
        //SubmitScore();
    }
    public void SubmitScore()
    {
        //Leaderboards.HIGHSCORES.SubmitScore((long)Enviroment.setDistance);
    }
    public void initialte_PauseMenu()
    {
       // var menu = Instantiate(UIpausemenu, new Vector3(550, 1000, transform.position.z), Quaternion.identity);
        //menu.transform.parent = Menu_.transform;
        UIpausemenu.SetActive(true);

        //Debug.Log("menh work");
    }

     public void HandleOnAdLoaded(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleAdLoaded event received");
       
        //Debug.Log("Run");
         MoveUI();
         


    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLeavingApplication event received");
        initialte_PauseMenu();
    }

    private void MoveUI(){
       
       
     
       UI_Score.position=locTemp.position;
        //.GetComponent<RectTransform>().position=new Vector3( OBJ[4].GetComponent<RectTransform>().position.x,-205f, OBJ[4].GetComponent<RectTransform>().position.z);
      // UI_Score.transform.position=new Vector3( UI_Score.position.x,-400f, UI_Score.position.z);   
    }

}
