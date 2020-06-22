using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIncrement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(true);
        StartCoroutine(Destroy_());
    }
     IEnumerator Destroy_(){
          yield return new WaitForSeconds(1.5f);
          Destroy(this.gameObject);

     }
   
}
