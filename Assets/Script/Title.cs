using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour
{
    public GameObject[] myPrefab;
    public Transform exit, ui_, startnew;
    public GameObject UI_m, ModeUI;
    public Camera cam;
    public string[] hexval;
    private bool inMode = false;
    // Start is called before the first frame update
    void Start()
    {
        TimeChange();
        StartCoroutine(Generate())
            ;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !inMode)
        {
            initialte_PauseMenu();
        }
        if (Input.GetKeyUp(KeyCode.Escape) && inMode)
        {
            // initialte_PauseMenu();
            UI_m.SetActive(true);
            ModeUI.SetActive(false);
            inMode = false;

        }
    }


    // Update is called once per frame


    IEnumerator Generate()
    {
        int Count = Random.Range(50, 80);
        for (int i = 0; i < Count; i++)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    Instantiate(myPrefab[0], new Vector3(Random.Range(Enviroment.maxX, Enviroment.minX), Random.Range(Enviroment.maxY, Enviroment.minY), 10), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(myPrefab[1], new Vector3(Random.Range(Enviroment.maxX, Enviroment.minX), Random.Range(Enviroment.maxY, Enviroment.minY), 10), Quaternion.identity);

                    break;
                case 3:
                    Instantiate(myPrefab[2], new Vector3(Random.Range(Enviroment.maxX, Enviroment.minX), Random.Range(Enviroment.maxY, Enviroment.minY), 10), Quaternion.identity);

                    break;
                case 4:
                    Instantiate(myPrefab[3], new Vector3(Random.Range(Enviroment.maxX, Enviroment.minX), Random.Range(Enviroment.maxY, Enviroment.minY), 10), Quaternion.identity);

                    break;
                default:

                    Instantiate(myPrefab[3], new Vector3(Random.Range(Enviroment.maxX, Enviroment.minX), Random.Range(Enviroment.maxY, Enviroment.minY), 10), Quaternion.identity);

                    break;
            }
        }
        yield return null;
    }
    public void LoadLevel(int level)
    {
       // int y = SceneManager.GetActiveScene().buildIndex;
       // SceneManager.UnloadSceneAsync(y);
        Enviroment.LoadLevel = level;
        SceneManager.LoadScene(0);
    }
    public void LoadLevel2(int level)
    {
        //int y = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.UnloadSceneAsync(y);
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            Enviroment.LoadLevel = 7;

        }
        else
        {
            Enviroment.LoadLevel = level;

        }
        SceneManager.LoadScene(0);
    }

    public void highscore()
    {
        Cloud.OnInitializeComplete += CloudOnceInitializeComplete;
        Cloud.Initialize(false, true);
    }
    public void CloudOnceInitializeComplete()
    {
        Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
    }
    public void initialte_PauseMenu()
    {
        var menu = Instantiate(exit, new Vector3(550, 730, transform.position.z), Quaternion.identity);
        menu.transform.parent = ui_.transform;
        //Debug.Log("menh work");
    }
    public void initialte_StartMenu()
    {
        var menu = Instantiate(startnew, new Vector3(550, 730, transform.position.z), Quaternion.identity);
        menu.transform.parent = ui_.transform;
        // Debug.Log("menh work");
    }
    private void TimeChange()
    {
        var today = System.DateTime.Now;
        int h = System.Int16.Parse(today.ToString("HH"));
        //a Debug.Log(""+h);

        int a = 0;
        //Morning
        if (h > 6 && h < 9)
        {
            a = 0;
        }
        //Night
        if (h > 22 && h < 6)
        {
            a = 4;
        }
        //Evening
        if (h > 9 && h < 1)
        {
            a = 1;
        }//mid day
        if (h > 1 && h < 18)
        {
            a = 2;
        }//dawn
        if (h > 18 && h < 22)
        {
            a = 3;
        }
        Color color;
        if (ColorUtility.TryParseHtmlString(hexval[a], out color))
        {
            cam.backgroundColor = color;
        }
    }
    public void LoadLevel_gameMode(int mode)
    {
        //int y = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.UnloadSceneAsync(y);

        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            Enviroment.LoadLevel = 7;
            PlayerPrefs.SetInt("tutorial", 1);

        }
        else
        {
            Enviroment.LoadLevel = 9;

        }
        Enviroment.Gamemod = mode;
        SceneManager.LoadScene(0);
    }
    public void newGame()
    {

        UI_m.SetActive(false);
        ModeUI.SetActive(true);
        inMode = true;

    }

    private void language()
    {

    }


}
