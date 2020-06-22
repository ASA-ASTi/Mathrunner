using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Cinemachine;


public class Human : MonoBehaviour
{
    Rigidbody2D rigidbody2D_;
    CapsuleCollider2D capsuleCollider2D;
    public float Jump_Velocity; //Jumping Speed 

    public float MoveSpeed; //Move Speed 
    private bool Direction;
    private float Temp;
    public GameObject Father;
    public bool FatherBehind = true;
    public bool lock1=false;


    //Button





    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D_ = transform.GetComponent<Rigidbody2D>();
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        Direction = true;
        InvokeRepeating("Speed_", 1.0f, 1f);
        InvokeRepeating("father_", 5.0f, 2f);
        lock1=true;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Enviroment.WalkP && lock1)
        {

            StartCoroutine(Move_PC());
        }

        //Distance




    }
    IEnumerator Distance(){
         Move_PC();
        Enviroment.setDistance = Enviroment.setDistance + ((DistanceCalculation() * Time.deltaTime));
 yield return null;
       
    }

    IEnumerator Move_PC()
    {
lock1=false;
        GetComponent<Animator>().enabled = true;

            Enviroment.setDistance = Enviroment.setDistance + ((DistanceCalculation() * Time.deltaTime));

        //Right Walking
        //animator.SetFloat("Movement",1.5f);
        rigidbody2D_.velocity = new Vector2(MoveSpeed, rigidbody2D_.velocity.y);

        //Walking Audio
        //WalkingEffect();
        lock1=true;
         yield return null;
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "object")
        {

            rigidbody2D_.velocity = Vector2.up * Jump_Velocity;


        }

    }
    float DistanceCalculation()
    {


        return (MoveSpeed * 1) / 5;
    }
    private void Speed_()
    {
        MoveSpeed = ((8 * Enviroment.setStamina) / 30);
    }
    private void father_()
    {
        if (Enviroment.setStamina < 10 && FatherBehind)
        {
            FatherBehind = false;
            Vector3 pos = new Vector3(transform.position.x - 5, 4.78f, transform.position.z);
            Father.GetComponent<Human2>().setpos(pos);//Vector3.MoveTowards(transform.position,pos, 8*Time.deltaTime);
                                                      //.position=;
                                                      //Moveto
            Father.SetActive(true);
            Enviroment.WalkC = true;
            Father.GetComponent<Animator>().enabled = true;
            Invoke("fatherinvole", 10f);
        }

    }
    private void fatherinvole()
    {
        FatherBehind = true;
    }

}



