using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionAnswers : MonoBehaviour
{
    public Text Question;
    public GameObject UI;
    public Text[] Answer;
    public Text LevelText;
    public Button[] Answerbtn;
    public GameObject LevelObject;
    int index = 0;
    public bool NewQuestion = true;
    public Transform LevelupLocation;
    public AudioClip[] Audios;
    public AudioSource audioSource;
    public int[,] Color_;
    // ="FFCC58";
    public string Color_Wrong = "FF0012";
    public string Color_Right = "41FF00";

    public float[] Daynight1;
    public float[] Daynight2;

    public float[] Daynight3;

    public static int daynightIndex = 1;
    public Camera background_;

    private float Timer = 0.0f;
    public  Question[] QuestionsBank;
    private static Question CurrentQuestion;
    List<Question> myList = new List<Question>();
    private bool Lock_1=false;
int level_Hard=0;


    private ArrayList myArryList = new ArrayList();// Recommended
    // Start is called before the first frame update
    private void Awake()
    {

 QuestionsBank = new Question[5];

        if (2 == SceneManager.GetActiveScene().buildIndex)
        {
             Enviroment.LoadGame();
             //Enviroment.Question_Generation=true;
               for (int i = 0; i < 5; i++)
            {
                QuestionsBank[i] = Generate();
            }
              if(!Enviroment.isEasy){
                Randomize(QuestionsBank);
            }
             Enviroment.Question_Generation=false;

        }
        else
        {
           
            Enviroment.Level_ = 1;
            Enviroment.CurrentLevel = 1;
            Enviroment.Question_Generation=false;
            NewQuestion=false;
             for (int i = 0; i < 5; i++)
            {
                QuestionsBank[i] = Generate();
            }

           
            
        }
    }
    void Start()
    {
        Audios[0].LoadAudioData();
        Audios[1].LoadAudioData();

        Color_ = new int[3, 3];
        Color_[0, 0] = 0;
        Color_[0, 1] = 0;
        Color_[0, 2] = 0;
        Color_[1, 0] = 255;
        Color_[1, 1] = 0;
        Color_[1, 2] = 18;
        Color_[2, 0] = 0;
        Color_[2, 1] = 255;
        Color_[2, 2] = 0;
        //Time.timeScale=0.5f;
       
        //Enviroment.Question_Generation = true;
        CurrentQuestion = new Question();
        InvokeRepeating("TimeChange",60f,60f);
        Lock_1=true;

       
        if(PlayerPrefs.GetInt("isEasy",0)==0){
             Enviroment.isEasy=true;
        }
        else{
              Enviroment.isEasy=false;
        }

        
    }
public  void Randomize<T>(T[] items)
    {
        System.Random rand = new System.Random();

        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < items.Length - 1; i++)
        {
            int j = rand.Next(i, items.Length);
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Enviroment.Question_Generation)
        {
            for (int i = 0; i < 5; i++)
            {
                QuestionsBank[i] = Generate();
            }
            if(!Enviroment.isEasy){
                Randomize(QuestionsBank);
            }
            Enviroment.Question_Generation = false;
        }
        if (NewQuestion && Lock_1)
        {

           
                StartCoroutine(Question_Thread());
           
           
           
            //Debug.Log(CurrentQuestion.Question_+"\n"+CurrentQuestion.trueanswer+"\n"+CurrentQuestion.Answer1+"\n"+CurrentQuestion.Answer2+"\n"+CurrentQuestion.Answer3+"\n"+CurrentQuestion.Answer4);
          
        }


    }
    IEnumerator Question_Thread(){
         Lock_1=false;
         NewQuestion = false;
         Enviroment.TimerSet = 0.0f;
            ResetButtonState();
            //   Debug.Log("Eqn"+AnswerEqn[index,0]+""+AnswerEqn[index,1]+""+AnswerEqn[index,2]+""+AnswerEqn[index,3]);
            // SetAnswer(QuestionEqn[index],""+AnswerEqn[index,0],""+AnswerEqn[index,1],""+AnswerEqn[index,2],""+AnswerEqn[index,3]);
            CurrentQuestion = QuestionsBank[index];
            //Debug.Log("Question ="+CurrentQuestion.Question_+"\nAnswer 1="+CurrentQuestion.Answer1+"\nAnswer 2="+CurrentQuestion.Answer2+"\nAnswer 3="+CurrentQuestion.Answer3+"\nAnswer 4="+CurrentQuestion.Answer4+"\nTrueAnswer ="+CurrentQuestion.trueanswer);
            if (Enviroment.CurrentLevel < CurrentQuestion.Level && Enviroment.isEasy)
            {
                Enviroment.CurrentLevel = CurrentQuestion.Level;
               // Debug.Log("LevelUp");
                Enviroment.SaveGame();
                //StartCoroutine(LevelUp(Enviroment.CurrentLevel+""));
                LevelIncrementIni();
            }
            else{
                Enviroment.CurrentLevel = Enviroment.Level_;
                Enviroment.SaveGame();

            }

            SetAnswer(CurrentQuestion);
            index++;
            if (index > 4)
            {
                Enviroment.Question_Generation = true;
                index = 0;
            }
            Lock_1=true;

        yield return null;
    }
    //Adding and Remove Functionlity


    //Function of Answer 

    //Button Click Of Answer


    public void Answer1Submit()
    {
        //Debug.Log("Submit1Clicked");
        if (AnswerCheck(CurrentQuestion.Answer1, CurrentQuestion.trueanswer))
        {
            StartCoroutine(PostProcessCorrectAnswer(0));
        }
        else
        {
            StartCoroutine(PostProcessWrongAnswer(0));
        }
    }
    public void Answer2Submit()
    {
        //  Debug.Log("Submit2Clicked");
        if (AnswerCheck(CurrentQuestion.Answer2, CurrentQuestion.trueanswer))
        {


            StartCoroutine(PostProcessCorrectAnswer(1));
        }
        else
        {
            StartCoroutine(PostProcessWrongAnswer(1));

        }
    }
    public void Answer3Submit()
    {
        // Debug.Log("Submit3Clicked");
        if (AnswerCheck(CurrentQuestion.Answer3, CurrentQuestion.trueanswer))
        {


            StartCoroutine(PostProcessCorrectAnswer(2));
        }
        else
        {
            StartCoroutine(PostProcessWrongAnswer(2));

        }
    }
    public void Answer4Submit()
    {
        // Debug.Log("Submit4Clicked");
        if (AnswerCheck(CurrentQuestion.Answer4, CurrentQuestion.trueanswer))
        {


            StartCoroutine(PostProcessCorrectAnswer(3));
        }
        else
        {
            StartCoroutine(PostProcessWrongAnswer(3));

        }
    }
    public void AnswerInput(int a)
    {
        //Debug.Log("AnswerInput=a ="+a);
        //Debug.Log("Question ="+CurrentQuestion.Question_+"\nAnswer 1="+CurrentQuestion.Answer1+"\nAnswer 2="+CurrentQuestion.Answer2+"\nAnswer 3="+CurrentQuestion.Answer3+"\nAnswer 4="+CurrentQuestion.Answer4+"\nTrueAnswer ="+CurrentQuestion.trueanswer);

        switch (a)
        {
            case 0:




                break;
            case 1:
                if (AnswerCheck(CurrentQuestion.Answer2, CurrentQuestion.trueanswer))
                {
                    StartCoroutine(PostProcessCorrectAnswer(a));
                }
                else
                {
                    StartCoroutine(PostProcessWrongAnswer(a));

                }
                break;
            case 2:
                if (AnswerCheck(CurrentQuestion.Answer3, CurrentQuestion.trueanswer))
                {
                    StartCoroutine(PostProcessCorrectAnswer(a));
                }
                else
                {
                    StartCoroutine(PostProcessWrongAnswer(a));

                }
                break;
            case 3:
                if (AnswerCheck(CurrentQuestion.Answer4, CurrentQuestion.trueanswer))
                {
                    StartCoroutine(PostProcessCorrectAnswer(a));
                }
                else
                {
                    StartCoroutine(PostProcessWrongAnswer(a));

                }
                break;
        }
    }
    private bool AnswerCheck(int a, int b)
    {

        // Debug.Log("Checking ="+a+"=="+b+"");
        if (a == b)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SetAnswer(Question Temp)
    {
        Question.text = Temp.Question_; //Painting Equation
        Answer[0].text = "" + Temp.Answer1;
        Answer[1].text = "" + Temp.Answer2;
        Answer[2].text = "" + Temp.Answer3;
        Answer[3].text = "" + Temp.Answer4;

    }
    public Question Generate()
    {
        return Enviroment.QuestionGenerator();
    }

    //Takes Place After Right Answer
    IEnumerator PostProcessCorrectAnswer(int ButtonNum)
    {
        audioSource.clip = Audios[0];
        audioSource.Play();

        //lit Green Button To Show Right Answer
        Answerbtn[ButtonNum].GetComponent<Image>().color = new Color(Color_[2, 0], Color_[2, 1], Color_[2, 2]);
        //DisableButton   
        SetButtonState();
        // Answer();
        if (Enviroment.isEasy)
        {
            Enviroment.setStamina = Enviroment.setStamina + (Enviroment.Level_);
        }
        else
        {
            Enviroment.setStamina = Enviroment.setStamina + (Enviroment.Level_ / Enviroment.TimerSet);
        }

        if (Enviroment.setStamina > 120)
        {
            Enviroment.setStamina = 120;
        }
        yield return new WaitForSeconds(0.2f);
        //StopAllCoroutines();
        NewQuestion = true;

    }
    //Takes Place After Wrong Answer
    IEnumerator PostProcessWrongAnswer(int ButtonNum)
    {
        audioSource.clip = Audios[1];
        audioSource.Play();
        //Debug.Log("Wrong Answer");
        Answerbtn[ButtonNum].GetComponent<Image>().color = new Color(Color_[1, 0], Color_[1, 1], Color_[1, 2]);

       // Debug.Log(CurrentQuestion.Question_ + "\n" + CurrentQuestion.trueanswer + "\n" + CurrentQuestion.Answer1 + "\n" + CurrentQuestion.Answer2 + "\n" + CurrentQuestion.Answer3 + "\n" + CurrentQuestion.Answer4);



        if (CurrentQuestion.Answer1 == CurrentQuestion.trueanswer) //(AnswerCheck(CurrentQuestion.Answer1, CurrentQuestion.trueanswer))
        {
            Answerbtn[0].GetComponent<Image>().color = new Color(Color_[2, 0], Color_[2, 1], Color_[2, 2]);
            // Debug.Log("true1")
        }

        if (CurrentQuestion.Answer2 == CurrentQuestion.trueanswer)//(AnswerCheck(CurrentQuestion.Answer2, CurrentQuestion.trueanswer))
        {
            Answerbtn[1].GetComponent<Image>().color = new Color(Color_[2, 0], Color_[2, 1], Color_[2, 2]);
        }

        if (CurrentQuestion.Answer3 == CurrentQuestion.trueanswer)//(AnswerCheck(CurrentQuestion.Answer3, CurrentQuestion.trueanswer))
        {
            Answerbtn[2].GetComponent<Image>().color = new Color(Color_[2, 0], Color_[2, 1], Color_[2, 2]);
        }

        if (CurrentQuestion.Answer4 == CurrentQuestion.trueanswer)//(AnswerCheck(CurrentQuestion.Answer4, CurrentQuestion.trueanswer))
        {
            Answerbtn[3].GetComponent<Image>().color = new Color(Color_[2, 0], Color_[2, 1], Color_[2, 2]);
        }



        //DisableButton   
        SetButtonState();
        // Answer();
        if (Enviroment.isEasy)
        {
            Enviroment.setStamina = Enviroment.setStamina - (Enviroment.Level_ / 2);
        }
        else
        {
            Enviroment.setStamina = Enviroment.setStamina - (Enviroment.Level_);

        }

        yield return new WaitForSeconds(0.2f);
        //StopAllCoroutines();
        NewQuestion = true;

    }
    //Reset Button to Default
    private void ResetButtonState()
    {
        Answerbtn[0].GetComponent<Button>().enabled = true;
        Answerbtn[1].GetComponent<Button>().enabled = true;
        Answerbtn[2].GetComponent<Button>().enabled = true;
        Answerbtn[3].GetComponent<Button>().enabled = true;
        for (int i = 0; i < 4; i++)
        {

            Answerbtn[i].GetComponent<Image>().color = new Color(Color_[0, 0], Color_[0, 1], Color_[0, 2]);
        }
    }
    //Set Lock Button
    private void SetButtonState()
    {
        Answerbtn[0].GetComponent<Button>().enabled = false;
        Answerbtn[1].GetComponent<Button>().enabled = false;
        Answerbtn[2].GetComponent<Button>().enabled = false;
        Answerbtn[3].GetComponent<Button>().enabled = false;
    }
    //Level Increment
    private void LevelIncrementIni()
    {
        GameObject e = Instantiate(LevelObject, LevelupLocation.transform.position, transform.rotation);
        e.GetComponentInChildren<Text>().text = "" + Enviroment.CurrentLevel;
        e.transform.SetParent(UI.transform);
        //Enviroment.MaxStamina = Enviroment.MaxStamina + Enviroment.CurrentLevel;
        //if (Enviroment.MaxStamina > 93)
       // /{
        //    Enviroment.MaxStamina = 93;
        //}
        if(Enviroment.isEasy && Enviroment.CurrentLevel>5){
            Enviroment.MaxStamina = 93;
        }
          else if(!Enviroment.isEasy && Enviroment.CurrentLevel<25){
                 Enviroment.MaxStamina = Enviroment.MaxStamina+3;
        }
        else if(!Enviroment.isEasy && Enviroment.CurrentLevel>29){
                 Enviroment.MaxStamina = 93;
        }



    }

    private void TimeChange()
    {

        
        // Color r=new Color(Daynight1[daynightIndex],Daynight2[daynightIndex],Daynight3[daynightIndex]);
        background_.backgroundColor = new Color(Daynight1[daynightIndex] / 255.0f, Daynight2[daynightIndex] / 255.0f, Daynight3[daynightIndex] / 255.0f);
       // Debug.Log("Changed Color");
        if (daynightIndex == 4)
        {
            daynightIndex = 0;

        }
        else
        {
            daynightIndex++;
        }
    }

}
