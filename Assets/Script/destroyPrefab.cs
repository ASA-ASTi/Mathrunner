using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPrefab : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("WOrking");
        if(col.gameObject.tag=="object"||col.gameObject.tag=="obs"){
                  Destroy(col.gameObject);

        }
    }
}
