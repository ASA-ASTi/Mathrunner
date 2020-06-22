using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerateObject : MonoBehaviour
{
    //public GameObject platform;
    public Transform[] Shape;
    public GameObject[] Platforms;
    public GameObject[] Tree;

    public int PlateformIndex;
    public int TreeIndex;


    //public Transform Tree;
    // public Transform Cloud;
    public Transform generationPoint;
    public float distanceBetween;
    private float platformWidth;
    //public Text Distance_;

    public float timerShape = 3;
    public float timerTree = 3;
    Color rndm;
    private bool lockPlatform, LockTree, LockShape;



    // Start is called before the first frame update
    void Start()
    {
        platformWidth = Platforms[0].GetComponent<BoxCollider2D>().size.x;
        lockPlatform = LockShape = LockTree = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  Debug.Log("Generating Platform");
        //rndm=


        timerShape = timerShape - Time.deltaTime;

        timerTree = timerTree - Time.deltaTime;

        if (transform.position.x < generationPoint.position.x && lockPlatform)
        {

StartCoroutine(GeneratePlatform());

        }
        if (timerShape < 0f && LockShape)
        {
            StartCoroutine(GenerateShape());

        }
        if (timerTree < 0f && LockTree)
        {
            StartCoroutine(GenerateTree());

        }


    }
    IEnumerator GeneratePlatform()
    {
        lockPlatform = false;
        transform.position = new Vector3(transform.position.x + platformWidth, 2.5f, transform.position.z);
        if (PlateformIndex > 14)
        {
            PlateformIndex = 0;
        }

        Platforms[PlateformIndex].transform.position = transform.position;
        Platforms[PlateformIndex].GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        // Debug.Log("Runninh" + PlateformIndex);
        //Instantiate(platform,transform.position,transform.rotation);
        //Algoritm.setDistance=Algoritm.setDistance+1;
        // Distance_.text=""+Algoritm.setDistance;
        PlateformIndex++;
        lockPlatform = true;

        yield return null;
    }
    IEnumerator GenerateShape()
    {
        LockShape = false;
        timerShape = Random.Range(1, 6);
        var t=  Instantiate(Shape[Random.Range(0, 8)], new Vector3(transform.position.x + platformWidth, transform.position.y + 0.5f, transform.position.z), transform.rotation);
       t.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        LockShape = true;

        yield return null;
    }
    IEnumerator GenerateTree()
    {
        LockTree=false;
        timerTree = Random.Range(1, 8);
            //Instantiate(Tree,new Vector3(transform.position.x+platformWidth+3f,transform.position.y+6f,transform.position.z),transform.rotation);
            if (TreeIndex > 4)
            {
                TreeIndex = 0;
            }
            Tree[TreeIndex].transform.position = new Vector3(transform.position.x + platformWidth + 3f, transform.position.y + 6f, transform.position.z);
            Tree[TreeIndex].GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            TreeIndex++;
                    LockTree=true;

        yield return null;
    }
}
