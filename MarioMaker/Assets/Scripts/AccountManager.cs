using System;
using TMPro;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    [HideInInspector] public int UserID = -1;
    [HideInInspector] public string UserName;
    
    [SerializeField] TMP_InputField usernameInputFieldSign;
    [SerializeField] TMP_InputField PaswrdInputFieldSign;
    [SerializeField] TMP_InputField usernameInputFieldLog;
    [SerializeField] TMP_InputField PaswrdInputFieldLog;

    public static AccountManager instance;
    
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    #region Account
    
    public void Signin()
    {
        //if signin valid
        //ask for id
        //UserID = ...
        UserName = usernameInputFieldSign.text;
    }

    public void Login()
    {
        //UserID = ...
        UserName = usernameInputFieldLog.text;
    }

    public void DeleteAccount()
    {
        if (UserID != -1)
        {
            //deleteAccount
        }
    }
    
    #endregion
    
    #region RemoteSaves

    public void UploadLvlToRemote()
    {
        
    }

    public void GetLvlJsonRemote()
    {
        
    }
    
    public void GetLvlInfosRemote()
    {
        
    }

    public void UpdateLvlRemote() //calls the delete waits for a positive result and upload functions
    {
        
    }
    
    public void DeleteLvlRemote()
    {
        
    }

    #endregion
}
