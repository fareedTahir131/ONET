using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class AuthManager : MonoBehaviour
{
    public static AuthManager instance;
    //public static Root root;
    public static string Token
    {
        set
        {
            PlayerPrefs.SetString("Token", value);
            Debug.Log(Token);
        }
        get
        {
            return PlayerPrefs.GetString("Token");
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
           //Destroy(gameObject);
        }
        else
        {
            instance = this;
           // DontDestroyOnLoad(this);
        }
    }


public static string BASE_URL = "http://13.53.93.155/";

    public void CreateUser(string name, string email, string password)
    {
        Debug.Log("Creating User");
        StartCoroutine(CreateUserStCoroutine(name,email,password));
    }

    public void LoginUser(string username, string password)
    {
        Debug.Log("Login User");
        StartCoroutine(LoginUserCoroutine(username, password));
    }


    IEnumerator CreateUserStCoroutine(string name, string email, string password)
    {
        //WWWForm form = new WWWForm();
        //form.AddField("name", name);
        //form.AddField("username", username);
        //form.AddField("email", email);
        //form.AddField("password", password);
        //form.AddField("color", Color);
        //form.AddField("gender", gender);
        //form.AddField("birthday", birthday);

        //string requestName = "api/v1/auth/sign_up";
        //using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + requestName, form))
        //{
        //    yield return www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        ConsoleManager.instance.ShowMessage("Network Error!");
        //        InputUIManager.instance.LoadingPanel.SetActive(false);
        //        Debug.Log(www.error);
        //    }
        //    else
        //    {
        //        Debug.Log(www.downloadHandler.text);
        //        OnSuccess(www.downloadHandler.text);
        //        SceneManager.LoadScene("Login");
        //    }
        //}


        Root root = new Root();
        User user = new User();

        user.email = email;
        user.password = password;
        user.name = name;

        root.user = user;

        string json = JsonUtility.ToJson(root);
        Debug.Log("json " + json);
        string requestName = "signup";
        var req = new UnityWebRequest(BASE_URL+ requestName, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
            ConsoleManager.instance.ShowMessage("Network Error!");
            InputUIManager.instance.LoadingPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            LoginResponce.Root Login_Responce = JsonUtility.FromJson<LoginResponce.Root>(req.downloadHandler.text);
            Debug.Log("message " + Login_Responce.status.message);
            InputUIManager.instance.LoadingPanel.SetActive(false);
            ConsoleManager.instance.ShowMessage(Login_Responce.status.message);
            InputUIManager.instance.SigninPanel.SetActive(true);
            InputUIManager.instance.SignupPanel.SetActive(false);
        }
    }
    IEnumerator LoginUserCoroutine(string email, string password)
    {
        //WWWForm form = new WWWForm();
        ////form.AddField("username", username);
        ////form.AddField("password", password);

        //string requestName = "signup";

        //using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + requestName, form))
        //{

        //    yield return www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        ConsoleManager.instance.ShowMessage("Network Error!");
        //        InputUIManager.instance.LoadingPanel.SetActive(false);
        //        Debug.Log(www.error);
        //    }
        //    else
        //    {
        //        OnSuccess(www.downloadHandler.text);
        //        SceneManager.LoadScene("VideoAR");
        //        //Debug.Log("token" + root.meta.token);//
        //    }
        //}

        // Create the request object
        //string requestName = "login";
        //UnityWebRequest request = UnityWebRequest.Post("http://13.53.93.155/login", "");

        // Create the request body data
        //var requestBody = new Dictionary<string, object>
        //{
        //    { "user", new Dictionary<string, string>
        //        {
        //            { "email", email },
        //            { "password", password },
        //            //{ "name", "" }
        //        }
        //    }
        //};


        Root root = new Root();
        User user = new User();

        user.email = email;
        user.password = password;
        user.name = "";

        root.user = user;

        string json = JsonUtility.ToJson(root);
        Debug.Log("json "+json);
        string requestName = "login";
        var req = new UnityWebRequest(BASE_URL + requestName, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError || req.isNetworkError)
        {
            if (req.responseCode == 401)
            {
                ConsoleManager.instance.ShowMessage("Invalid Email or Password!");
                InputUIManager.instance.LoadingPanel.SetActive(false);
            }
            else
            {
                Debug.Log("Error While Sending: " + req.error);
                ConsoleManager.instance.ShowMessage("Network Error!");
                InputUIManager.instance.LoadingPanel.SetActive(false);
            }
            
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            LoginResponce.Root Login_Responce = JsonUtility.FromJson<LoginResponce.Root>(req.downloadHandler.text);
            Debug.Log("message " + Login_Responce.status.message);
            InputUIManager.instance.LoadingPanel.SetActive(false);
            ConsoleManager.instance.ShowMessage(Login_Responce.status.message);
            if (Login_Responce.status.message== "Logged in successfully.")
            {
                SceneManager.LoadScene(1);
            }
        }
    }


    private void OnSuccess(string json)
    {
        //Debug.Log("Json "+json);
        //root = JsonUtility.FromJson<Root>(json);
        //Debug.Log("Login Success Function");
        //Debug.Log(root.meta.token);
        //Token = root.meta.token;
        
        //ProfileManager.FullName = root.user.name;
        //ProfileManager.UserName = root.user.username;
        //ProfileManager.UserID = root.user.id;
        //ProfileManager.UserEmail = root.user.email;
        //ProfileManager.UserImageUrl = root.user.image_url;
        ////DateTime Birthday = DateTimeOffset.Parse(root.user.birthday.ToString()).DateTime;
        ////ProfileManager.UserAge = GetAge(Birthday).ToString();
        //Debug.Log("ProfileManager.UserAge " + ProfileManager.UserAge);
        //Debug.Log("UserEmail " + ProfileManager.UserEmail);
    }
    public static int GetAge(DateTime birthDate)
    {
        DateTime n = DateTime.Now; // To avoid a race condition around midnight
        int age = n.Year - birthDate.Year;

        if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
            age--;

        return age;
    }
}