using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
//using Cinemachine;


public class Human_mul2 : MonoBehaviour
{
    Rigidbody2D rigidbody2D_;
    CapsuleCollider2D capsuleCollider2D;
    public float MoveSpeed; //Move Speed 
    private float pos_;
    public float distance;
    private float Temp;
    public bool Start_Game = false;
    public GameObject TextName;


    // Start is called befor=e the first frame update
    void Start()
    {
        TextName.GetComponent<MeshRenderer>().sortingOrder=5;
        rigidbody2D_ = transform.GetComponent<Rigidbody2D>();
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
                     GetComponent<Animator>().enabled = true;

        //Direction = true;
        // database = FirebaseDatabase.DefaultInstance;
        //fetchingPlayer=database.GetReference("Users/Gamerooms/" + hostName + "/Multiplayer/Game_details");
        //fetchingPlayer.ValueChanged+=fetching_ReadyState;
        //InvokeRepeating("UploadScores", 0.5f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Start_Game)
        {
          //  controller_player();
            //Enviroment.setDistance = Enviroment.setDistance + ((DistanceCalculation() * Time.deltaTime));
            //UploadScores();
           this.gameObject.transform.position = Vector2.MoveTowards(transform.position, new Vector2(pos_,this.transform.position.y),MoveSpeed*Time.deltaTime);


        }

    }

    private void controller_player()
    {

        GetComponent<Animator>().enabled = true;


        //Right Walking
        //animator.SetFloat("Movement",1.5f);
        rigidbody2D_.velocity = new Vector2(MoveSpeed, rigidbody2D_.velocity.y);
        GetComponent<SpriteRenderer>().flipX = false;

        //Walking Audio
        //WalkingEffect();
    }
    public void SetData(float moveSpeed, bool gameOver,float Distance,float x)
    {
        this.MoveSpeed = moveSpeed;
       // this.gameObject.transform.position=new Vector3(x,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
       this.distance=Distance;
       this.pos_=x;
        if (gameOver)
        {
            //Start_Game==false;
            Start_Game = this.gameObject.GetComponent<Animator>().enabled = false;

        }
    }
  public void   setName(string abc){
      TextName.GetComponent<TextMesh>().text=abc;
  }





}
