using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public Transform Circle;
    public int VerticalLocation=50;
    public float HorizontalLocation;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<VerticalLocation;i=i+3){
            transform.position=new Vector3(Random.Range(-2.0f,2.0f),i,-2);
            Instantiate(Circle,transform.position,transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
