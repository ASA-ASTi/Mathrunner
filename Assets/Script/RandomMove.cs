using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
     
     
     private float tChange=0f; // force new direction in the first Update
     private float randomX;
     private float randomY;
     public float moveSpeed;
     
     void Start()
    {
        moveSpeed=Random.Range(3,10);
    }
     void Update () {
         // change to random direction at random intervals
         if (Time.time >= tChange){
             randomX = Random.Range(-2.0f,2.0f); // with float parameters, a random float
             randomY = Random.Range(-2.0f,2.0f); //  between -2.0 and 2.0 is returned
             // set a random interval between 0.5 and 1.5
             tChange = Time.time + Random.Range(0.5f,1.5f);
         }

         Vector2 Temp=new Vector2(randomX,randomY);

         this.transform.Translate(Temp * moveSpeed * Time.deltaTime);
         // if object reached any border, revert the appropriate direction
         if (transform.position.x >= Enviroment.maxX || transform.position.x <= Enviroment.minX) {
            randomX = -randomX;
         }
         if (transform.position.y >= Enviroment.maxY || transform.position.y <= Enviroment.minY) {
            randomY = -randomY;
         }
         // make sure the position is inside the borders

         Vector2 r=new Vector2(Mathf.Clamp(transform.position.x, Enviroment.minX, Enviroment.maxX),Mathf.Clamp(transform.position.y, Enviroment.minY, Enviroment.maxY));
         transform.position=r;
     }
         
}
