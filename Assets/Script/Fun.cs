using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fun : MonoBehaviour
{
    public GameObject[] Platforms;
    public Transform generationPoint;
    private float platformWidth;
    public int PlateformIndex = 0;
    public float[] x_;
    public float[] y_;

    // Start is called before the first frame update
    void Start()
    {
        platformWidth = Platforms[0].GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Generating Platform");
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth, 2.5f, transform.position.z);
            if (PlateformIndex > 5)
            {
                PlateformIndex = 0;
            }

            Platforms[PlateformIndex].transform.position = transform.position; //0






            //Platforms[PlateformIndex+1].transform.position=transform.position;
            Debug.Log("Runninh" + PlateformIndex);
            //Instantiate(platform,transform.position,transform.rotation);
            //Algoritm.setDistance=Algoritm.setDistance+1;
            // Distance_.text=""+Algoritm.setDistance;
            PlateformIndex++;

        }

    }
}




