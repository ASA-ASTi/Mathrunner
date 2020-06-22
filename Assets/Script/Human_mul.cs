using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
//using Cinemachine;


public class Human_mul : MonoBehaviour
{
    Rigidbody2D rigidbody2D_;
    CapsuleCollider2D capsuleCollider2D;
    public float Jump_Velocity; //Jumping Speed 
    public float MoveSpeed; //Move Speed 
    private bool Direction;
    private float Temp;
    public GameObject Father;
    public bool FatherBehind = true;
    private FirebaseDatabase database;
    private DatabaseReference fetchingPlayer;
    public GameObject TextName;

    public bool Start_Game = false;
    private bool WalkLock;
    public bool TimeOut=false;

    // Start is called befor=e the first frame update
    void Start()
    {

        TextName.GetComponent<MeshRenderer>().sortingOrder=5;
        TextName.GetComponent<TextMesh>().text=""+Enviroment.Name;

        rigidbody2D_ = transform.GetComponent<Rigidbody2D>();
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        Direction = true;
        InvokeRepeating("Speed_", 1.0f, 1f);
        database = FirebaseDatabase.DefaultInstance;
        WalkLock=true;
        // fetchingPlayer=database.GetReference("Users/Gamerooms/" + hostName + "/Multiplayer/Game_details");
        //fetchingPlayer.ValueChanged+=fetching_ReadyState;
        //InvokeRepeating("UploadScores", 0.5f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Start_Game & TimeOut)
        {
            if(WalkLock){
                WalkLock=false;
                StartCoroutine(WalkProcess());
  
            }
          

        }
       if(TimeOut){
            UploadData(MoveSpeed,Start_Game);

       }
        
         


    }
    IEnumerator WalkProcess(){
         controller_player();
            Enviroment.setDistance = Enviroment.setDistance + ((DistanceCalculation() * Time.deltaTime));
           
        WalkLock=true;
        yield return null;
    }

    private void controller_player()
    {

        GetComponent<Animator>().enabled = true;


        //Right Walking
        //animator.SetFloat("Movement",1.5f);
        rigidbody2D_.velocity = new Vector2(MoveSpeed, rigidbody2D_.velocity.y);
        GetComponent<SpriteRenderer>().flipX = false;
        Direction = true;

        //Walking Audio
        //WalkingEffect();
    }

    float DistanceCalculation()
    {


        return (MoveSpeed * 1) / 5;
    }
    private void Speed_()
    {
        MoveSpeed = ((8 * Enviroment.setStamina) / 30);
    }
    public void UploadData(float movespeed,bool gameOver)
    {

        location xyz = new location();
       // xyz.distance = Enviroment.setDistance;
        xyz.movespeed = MoveSpeed;
        xyz.distance=Enviroment.setDistance;
        xyz.x=this.gameObject.transform.position.x;
        xyz.gameOver=!gameOver;
        string json = JsonUtility.ToJson(xyz);
        database.GetReference("Users/Gamerooms/" + Enviroment.HostName + "/Multiplayer/PlayersLocation/"+Enviroment.Username).SetRawJsonValueAsync(json);

    }
   
        

    

}
