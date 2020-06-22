using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;
using UnityEngine.UI;
     using UnityEngine.SceneManagement;
     using System.Collections;
     using UnityEngine.Networking;


public class GoogleSignInDemo : MonoBehaviour

{  Color c,d;
    public GameObject bt;
    public Camera cam;
    public Text infoText;
    public string webClientId = "<your client id here>";

    private FirebaseAuth auth,mAuth;
    private GoogleSignInConfiguration configuration;
    private void Awake()
    { 
        if(!Enviroment.Login){
 configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        CheckFirebaseDependencies();
        }
       
       // mAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
     private void Start() {
         
         bt.SetActive(false);
         if(!Enviroment.Login){
         Invoke("SignInWithGoogle",1f);

         }
         else{
SceneManager.LoadScene(5);
         }

    }
    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)){
            Enviroment.LoadLevel=1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    auth = FirebaseAuth.DefaultInstance;
                    }
            
                else{
                      bt.SetActive(true);
                }
                   // AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            else
            {                       bt.SetActive(true);

                //AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
       // AddToInformation("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignOut()
    {
      //  AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
      //  AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                   // AddToInformation("Got Error: " + error.Status + " " + error.Message);
                                         bt.SetActive(true);

                }
                else
                {
                   // AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                                         bt.SetActive(true);

                }
            }
        }
        else if (task.IsCanceled)
        {
            //AddToInformation("Canceled");
             infoText.text="Please Click On Sign in Button";
                                   bt.SetActive(true);

        }
        else
        {
           
            SignInWithGoogleOnFirebase(task.Result.IdToken);
            //AddToInformation("After functionFirebase");

        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        //AddToInformation("Firebase");
        Credential credential = GoogleAuthProvider.GetCredential(idToken,null);
        //infoText.text="Firebase";
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0)){
                     infoText.text="Please Click On Sign in Button";
                                           bt.SetActive(true);

                }
                    //AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
                    //infoText.text="\nError code = " + inner.ErrorCode + " Message = " + inner.Message;
            
            
            }
            else
            {
                //AddToInformation("Sign IN");
                //infoText.text="Sign in";
                Enviroment.Username= task.Result.Email;
                //Enviroment.profilepicture=task.Result.
                Enviroment.Name=task.Result.DisplayName;
                StartCoroutine(setImage(task.Result.PhotoUrl)); 
            }
        });
         
    }

    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
       // AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        //AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    //private void AddToInformation(string str) { //i//nfoText.text += "\n" + str; }
    


   IEnumerator setImage(Uri url) {
        //AddToInformation("pic");
        Enviroment.uri=url+"";
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
    {
        yield return uwr.SendWebRequest();
     
        if (uwr.isNetworkError || uwr.isHttpError)
        {
           //- AddToInformation("error");

        }
        else
        {
            Texture2D tex = new Texture2D(2, 2);
             tex = DownloadHandlerTexture.GetContent(uwr);
     
             Enviroment.profilepicture = tex;
             Enviroment.Login=true;
              SceneManager.LoadScene(5);
        }
    }
        
  
                   
}void colorchange(){
            

            
           if(cam.backgroundColor==c){
               cam.backgroundColor=Color.Lerp(c,d,Time.deltaTime*2);
           }
            else{
               cam.backgroundColor=Color.Lerp(d,c,Time.deltaTime*2);
           }
           // Debug.Log("running");
        }
}