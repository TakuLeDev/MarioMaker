using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AccountManager : MonoBehaviour
{
    [Header("user infos")]
    public string UserID = "";
    public string UserName = "";
    public string AccessToken = "";
    public string RefreshToken = "";
    
    [Header("login")]
    [SerializeField] private string apiBaseUrl = "http://127.0.0.1:8000/";

    [Header("Signin")]
    [SerializeField] TMP_InputField usernameInputFieldSign;
    [SerializeField] TMP_InputField passwordInputFieldSign;

    [Header("Login")]
    [SerializeField] TMP_InputField usernameInputFieldLog;
    [SerializeField] TMP_InputField passwordInputFieldLog;

    public static AccountManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // =========================================================
    // DATA CLASSES
    // =========================================================

    [Serializable]
    public class AuthData
    {
        public string username;
        public string email;
        public string password;
    }

    [Serializable]
    public class LoginResponse
    {
        public string refresh;
        public string access;
        public string user_id;
        public string username;
    }

    [Serializable]
    public class LevelUploadData
    {
        public string name;
        public string json;
    }

    // =========================================================
    // ACCOUNT
    // =========================================================

    public void StartSignin()
    {
        StartCoroutine(Signin());
    }

    IEnumerator Signin()
    {
        AuthData data = new AuthData
        {
            username = usernameInputFieldSign.text,
            email = "",
            password = passwordInputFieldSign.text
        };

        string json = JsonUtility.ToJson(data);

        using UnityWebRequest request =
            new UnityWebRequest(apiBaseUrl + "user/signin/", "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            LoginResponse response =
                JsonUtility.FromJson<LoginResponse>(
                    request.downloadHandler.text);

            UserID = response.user_id;
            UserName = data.username;

            AccessToken = response.access;
            RefreshToken = response.refresh;

            Debug.Log("Compte créé");
            Debug.Log("UserID : " + UserID);
        }
        else
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
    }

    public void StartLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        AuthData data = new AuthData
        {
            username = usernameInputFieldLog.text,
            password = passwordInputFieldLog.text
        };

        string json = JsonUtility.ToJson(data);

        using UnityWebRequest request =
            new UnityWebRequest(apiBaseUrl + "user/login/", "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Connexion réussie");

            LoginResponse response =
                JsonUtility.FromJson<LoginResponse>(
                    request.downloadHandler.text);

            UserID = response.user_id;
            UserName = response.username;

            AccessToken = response.access;
            RefreshToken = response.refresh;

            Debug.Log("UserID : " + UserID);
            Debug.Log("Access token : " + AccessToken);
        }
        else
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
    }
    
    public void Logout()
    {
        UserID = "";
        UserName = "";

        AccessToken = "";
        RefreshToken = "";

        Debug.Log("Déconnexion réussie");
    }

    public void StartDeleteAcc()
    {
        StartCoroutine(DeleteAccount());
    }

    IEnumerator DeleteAccount()
    {
        if (string.IsNullOrEmpty(UserID))
            yield break;

        string url = apiBaseUrl + "user/delete/" + UserID + "/";

        using UnityWebRequest request = UnityWebRequest.Delete(url);

        request.SetRequestHeader( "Authorization", "Bearer " + AccessToken );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Compte supprimé");

            Logout();
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    // =========================================================
    // LEVELS
    // =========================================================

    //menu not tested
    public void StartCreateLevel(string levelName, string levelJson)
    {
        StartCoroutine(CreateLevel(levelName, levelJson));
    }

    IEnumerator CreateLevel(string levelName, string levelJson)
    {
        LevelUploadData data = new LevelUploadData
        {
            name = levelName,
            json = levelJson
        };

        string json = JsonUtility.ToJson(data);

        using UnityWebRequest request =
            new UnityWebRequest(apiBaseUrl + "levels/", "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader( "Authorization", "Bearer " + AccessToken );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Niveau créé");
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
    }

    //not tested
    public IEnumerator GetLvlJson(string lvlID, Action<string> callback)
    {
        string url = apiBaseUrl + "levels/" + lvlID + "/json/";

        using UnityWebRequest request = UnityWebRequest.Get(url);
        
        request.SetRequestHeader( "Authorization", "Bearer " + AccessToken );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback?.Invoke(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    //not tested
    public IEnumerator GetLvlInfos(int nmbr, Action<string> callback)
    {
        string url = apiBaseUrl + "levels/latest/" + nmbr + "/";

        using UnityWebRequest request = UnityWebRequest.Get(url);
        
        request.SetRequestHeader( "Authorization", "Bearer " + AccessToken );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback?.Invoke(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    //not tested
    public void StartUpdateLvl(string lvlID)
    {
        StartCoroutine(UpdateLvl(lvlID));
    }

    IEnumerator UpdateLvl(string lvlID)
    {
        string url = apiBaseUrl + "levels/" + lvlID + "/update/";

        string levelJson = LevelManager.instance.TilemapToString();

        string body =
            "{\"json\":" + levelJson + "}";

        using UnityWebRequest request =
            UnityWebRequest.Put(url, body);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader( "Authorization", "Bearer " + AccessToken );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Niveau mis à jour");
        }
        else
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
    }

    //not tested
    public void StartDeleteLvl(string lvlID)
    {
        StartCoroutine(DeleteLvl(lvlID));
    }
    
    IEnumerator DeleteLvl(string lvlID)
    {
        string url = apiBaseUrl + "levels/" + lvlID + "/delete/";

        using UnityWebRequest request = UnityWebRequest.Delete(url);
        
        request.SetRequestHeader( "Authorization", "Bearer " + AccessToken );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Niveau supprimé");
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
    
    // =========================================================
    // UTILITY
    // =========================================================

    public void ClearFields()
    {
        usernameInputFieldSign.text = "";
        usernameInputFieldLog.text = "";
        passwordInputFieldSign.text = "";
        passwordInputFieldLog.text = "";
            
    }
}