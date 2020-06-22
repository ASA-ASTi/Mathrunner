using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Cinemachine;


public class Human3 : MonoBehaviour
{
    Rigidbody2D rigidbody2D_;
    CapsuleCollider2D capsuleCollider2D;
     public float Jump_Velocity; //Jumping Speed 

    public float MoveSpeed; //Move Speed 
    private bool Direction;
    public GameObject Player_;


    //Button

  



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D_=transform.GetComponent<Rigidbody2D>();
        capsuleCollider2D=transform.GetComponent<CapsuleCollider2D>();
        Direction=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enviroment.WalkC){
          Move_PC();
        }
      

      
    }

    
    private void Move_PC(){
        
               GetComponent<Animator>().enabled=true;
      
            
                //Right Walking
                 //animator.SetFloat("Movement",1.5f);
                 rigidbody2D_.velocity=new Vector2(MoveSpeed,rigidbody2D_.velocity.y);
                 GetComponent<SpriteRenderer>().flipX=false;
                 Direction=true;

                  //Walking Audio
            //WalkingEffect();
    }
       void OnCollisionEnter2D(Collision2D col)
    {
         
          
    }

    
}
