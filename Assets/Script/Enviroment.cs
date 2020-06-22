using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using CloudOnce;
using System.Collections.Generic;


public static class Enviroment
{

    private static int level = 1;  //Level
    private static int Alevel = 1;    //LevelAnother
    private static int trueAnswer;
    public static bool AdInitialise = false;
    private static int RetryLevel = 3;
    public static int Gamemod = 0;
    public static bool Login = false;
    public static bool isEasy = true;
    public static int LoadLevel = 1;
    static int diffrence = 13;
    static bool PlayerWalking = true;
    static bool ComputerWalking = true;
    public static float MaxStamina = 20;
    public static float maxX = 3.0f;
    public static float minX = -3.0f;
    public static float maxY = 5.5f;
    public static float minY = -5.5f;
    private static float Distance = 0;
    public static string Name;
    private static bool GameOver = false;
    private static float Stamina = 30;
    public static Texture profilepicture;
    private static bool QuestionGenerate = false;
    private static float Timer = 0.0f;
    private static string Username_;
    public static string uri;
    public static int language = 0;
    public static string HostName;
    public static long multiplayerId;
    public static string Username
    {
        set
        {
            Username_ = value.Replace("@gmail.com", "");
        }
        get
        {
            return Username_;
        }
    }
    public static float TimerSet
    {
        set
        {
            Timer = value;
        }
        get
        {
            return Timer;
        }
    }
    public static int Retry
    {
        set
        {
            RetryLevel = 3;
        }
        get
        {
            return RetryLevel;
        }
    }
    public static int Level_
    {
        set
        {
            level = value;
        }
        get
        {
            return level;
        }
    }
    public static int ALevel_
    {
        set
        {
            Alevel = value;
        }
        get
        {
            return Alevel;
        }
    }
    public static bool Question_Generation
    {
        set
        {
            QuestionGenerate = value;

        }
        get
        {
            return QuestionGenerate;
        }
    }
    public static float setStamina
    {
        set
        {
            Stamina = value;
            if (Stamina > MaxStamina)
            {
                Stamina = MaxStamina;
            }
            else if (Stamina < 0)
            {
                Stamina = 0;
            }
        }
        get
        {
            return Stamina;
        }
    }
    public static bool setGameOver
    {
        set
        {
            GameOver = value;
        }
        get
        {
            return GameOver;
        }
    }
    public static float setDistance
    {
        get
        {
            return Distance;
        }
        set
        {
            Distance = value;
        }
    }
    public static int CurrentLevel = 1;
    public static int trueAnswer_
    {
        get
        {
            return trueAnswer;
        }
    }
    public static bool WalkP
    {
        get
        {
            return PlayerWalking;
        }
        set
        {
            PlayerWalking = value;
        }
    }
    public static bool WalkC
    {
        get
        {
            return ComputerWalking;
        }
        set
        {
            ComputerWalking = value;
        }
    }
    //Methods
    public static string RandomSignGenerate(int a, int b)
    {
        ArrayList myArryList = new ArrayList();// Recommended
        int i;
        i = Random.Range(1, 9);

        //Addition
        if (i == 1 || i == 5)
        {

            return "+";
        }
        //Addition
        if (i == 2 || i == 6)
        {
            return "-";
        }
        //Addition
        if (i == 3 || i == 7)
        {
            return "*";
        }
        //Addition
        if (i == 4 || i == 8)
        {
            return "/";
        }
        else
        {
            return "+";
        }



    }
    public static Question QuestionGenerator()
    {
        string str;
        Question q1 = new Question();
    //Can Proceed
    Equation:

        string Sign;
        if (level >= Alevel)
        {
            if (Gamemod == 0)
            {
                Sign = RandomSignGenerate(level, Alevel);// Recommende
                if (Sign.Equals("+"))
                {
                    q1.trueanswer = level + Alevel;
                }
                else if (Sign.Equals("-"))
                {
                    q1.trueanswer = level - Alevel;
                }
                else if (Sign.Equals("*"))
                {
                    q1.trueanswer = level * Alevel;
                }
                else
                {
                    q1.trueanswer = level / Alevel;
                }
            }
            else if (Gamemod == 1)
            {
                Sign = "+";
                q1.trueanswer = level + Alevel;

            }
            else if (Gamemod == 2)
            {
                Sign = "-";
                q1.trueanswer = level - Alevel;


            }
            else if (Gamemod == 3)
            {
                Sign = "*";
                q1.trueanswer = level * Alevel;

            }
            else
            {
                Sign = "/";
                q1.trueanswer = level / Alevel;

            }
            // int Temp;


            str = level + "" + Sign + "" + Alevel;
            q1.Question_ = str;

            ArrayList Variation;
            /*if(float_){
             float temp=float.Parse(q1.trueanswer);
             temp.ToString("0.00");
             Variation= AnswerVariationGeneration_float(temp);

            }
            else{
             Variation = AnswerVariationGeneration_long(Int64.Parse(q1.trueanswer));
            
           
            }*/

            Variation = AnswerVariationGeneration(q1.trueanswer);
            q1.Answer1 = (int)Variation[0];
            q1.Answer2 = (int)Variation[1];
            q1.Answer3 = (int)Variation[2];
            q1.Answer4 = (int)Variation[3];
            q1.Level = level;
            //return str;
            Alevel++;

        }
        else
        {
            level++;
            Alevel = 1;
            goto Equation;
        }

        return q1;

    }

    public static ArrayList AnswerVariationGeneration(int RealAnswer)
    {


        int Answer1;
        int Answer2;
        int Answer3;
        ArrayList myArryList = new ArrayList();// Recommended
    Work:
        int count = 0;
        int Addition = Random.Range(1, diffrence);

        //Prefix Algorithm
        int f = Random.Range(1, 13);
        if (f == 1 || f == 5 || f == 9)
        {
            int flag = RealAnswer - Addition;
            //Generating Random prefix FalseAnswer1
            Answer1 = Random.Range(flag, RealAnswer);
        //Label To Generate Again
        reRead:
            //Generating Random prefix FalseAnswer2
            Answer2 = Random.Range(flag, RealAnswer);

            Answer3 = Random.Range(flag, RealAnswer);

            //Checking Both Answer is not Same
            if (Answer1 == Answer2 || Answer1 == Answer3 || Answer3 == Answer2)
            {
                if (count > 6)
                {
                    goto Work;
                }
                count++;
                goto reRead;
            }



        }
        //Infix Algorithm
        else if (f == 2 || f == 6 || f == 10)
        {
            int flag1 = RealAnswer - (Addition / 2);
            int flag2 = RealAnswer + (Addition / 2);

            //Generating Random prefix FalseAnswer1
            Answer1 = Random.Range(flag1, RealAnswer);
        //Label To Generate Again
        reRead:
            //Generating Random prefix FalseAnswer2
            Answer2 = Random.Range(RealAnswer + 1, flag2 + 1);
            Answer3 = Random.Range(RealAnswer - 10, RealAnswer + 10);

            //Checking Both Answer is not Same
            if (Answer1 == Answer2 || Answer1 == Answer3 || Answer3 == Answer2)
            {
                if (count > 6)
                {
                    goto Work;
                }
                count++;
                goto reRead;
            }



        }
        //Postfix Algorithm
        else if (f == 3 || f == 7 || f == 11)
        {
            int flag = RealAnswer + Addition;

            //Generating Random postfix FalseAnswer1
            Answer1 = Random.Range(RealAnswer + 1, flag + 1);
        //Label To Generate Again
        reRead:
            //Generating Random postfix FalseAnswer2
            Answer2 = Random.Range(RealAnswer + 1, flag + 1);
            Answer3 = Random.Range(RealAnswer - 10, RealAnswer + 10);

            //Checking Both Answer is not Same
            if (Answer1 == Answer2 || Answer1 == Answer3 || Answer3 == Answer2)
            {
                if (count > 6)
                {
                    goto Work;
                }
                count++;
                goto reRead;
            }



        }
        //Random Algorithm
        else if (f == 4 || f == 8 || f == 12)
        {
            //int flag=trueAnswer+Addition;

            //Generating Random postfix FalseAnswer1
            Answer1 = Random.Range(RealAnswer - 15, RealAnswer + 15);
        //Label To Generate Again
        reRead:
            //Generating Random postfix FalseAnswer2
            Answer2 = Random.Range(RealAnswer - 14, RealAnswer + 16);
            Answer3 = Random.Range(RealAnswer - 20, RealAnswer + 21);

            //Checking Both Answer is not Same
            if (Answer1 == Answer2 || Answer1 == Answer3 || Answer3 == Answer2)
            {
                if (count > 6)
                {
                    goto Work;
                }
                count++;
                goto reRead;
            }



        }
        else
        {
            goto Work;



        }
        myArryList.Add(RealAnswer);
        myArryList.Add(Answer1);
        myArryList.Add(Answer2);
        myArryList.Add(Answer3);



        for (int i = 0; i < myArryList.Count; i++)
        {
            int temp = (int)myArryList[i];
            int randomIndex = UnityEngine.Random.Range(i, myArryList.Count);
            myArryList[i] = myArryList[randomIndex];
            myArryList[randomIndex] = temp;
        }

        return myArryList;
    }

    public static ArrayList Answers(ArrayList myArryList)
    {
        for (int i = 0; i < myArryList.Count; i++)
        {
            int temp = (int)myArryList[i];
            int randomIndex = UnityEngine.Random.Range(i, myArryList.Count);
            myArryList[i] = myArryList[randomIndex];
            myArryList[randomIndex] = temp;
        }



        return myArryList;
        /*int val=Random.Range(1,17);

        if(val==1||val==8||val==9||val==16){
            myArryList.Add(Answer1);
            myArryList.Add(trueAnswer);
            myArryList.Add(Answer2);
            myArryList.Add(Answer3);

            return myArryList;
        }
        else if(val==2||val==7||val==10||val==15){
            myArryList.Add(trueAnswer);
            myArryList.Add(Answer1);
            myArryList.Add(Answer2);
            myArryList.Add(Answer3);
            return myArryList;
        }
        else if(val==3||val==6||val==11||val==14){
            myArryList.Add(Answer3);
            myArryList.Add(Answer1);
            myArryList.Add(trueAnswer);
            myArryList.Add(Answer2);
            return myArryList;
        }
        else if(val==4||val==5||val==12||val==13){
            myArryList.Add(Answer3);
            myArryList.Add(Answer1);
            myArryList.Add(Answer2);
            myArryList.Add(trueAnswer);
            return myArryList;
        }
        else{
            myArryList.Add(Answer3);
            myArryList.Add(Answer1);
            myArryList.Add(Answer2);
            myArryList.Add(trueAnswer);
            return myArryList;
        }
        */
    }
    public static int DecrementRetry()
    {
        RetryLevel = RetryLevel - 1;
        return RetryLevel;
    }
    public static void SaveGame()
    {
        if (Gamemod == 0)
        {
            PlayerPrefs.SetInt("LevelMix", CurrentLevel);

        }
        else if (Gamemod == 1)
        {
            PlayerPrefs.SetInt("LevelAdd", CurrentLevel);

        }
        else if (Gamemod == 2)
        {
            PlayerPrefs.SetInt("LevelSub", CurrentLevel);

        }
        else if (Gamemod == 3)
        {
            PlayerPrefs.SetInt("LevelMul", CurrentLevel);

        }
        else
        {
            PlayerPrefs.SetInt("LevelDiv", CurrentLevel);

        }
        PlayerPrefs.SetInt("TempLevel", CurrentLevel);


        if (PlayerPrefs.GetFloat("HighHike", Distance) < Distance)
        {
            PlayerPrefs.SetFloat("HighHike", Distance);
        }
        PlayerPrefs.SetInt("gameMode", Gamemod);



    }
    public static void LoadGame()
    {

        level = PlayerPrefs.GetInt("TempLevel");
        Gamemod = PlayerPrefs.GetInt("gameMode");

        if (level == 0)
        {
            level = 1;

        }
        else
        {
            CurrentLevel = level;
        }
     
    }




}
