using System.Collections.Generic;
using UnityEngine;

public class LevelInfoHolder : MonoBehaviour
{
    //used to transfer already loaded info to the lvl scene and to get the missing values from the database
    [HideInInspector] public int lvlID;
    [HideInInspector] public string lvlName;
    [HideInInspector] public int lvlLikes;
    
    public static LevelInfoHolder instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
