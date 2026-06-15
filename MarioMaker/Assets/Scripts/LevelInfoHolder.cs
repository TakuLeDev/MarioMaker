using UnityEngine;

public class LevelInfoHolder : MonoBehaviour
{
    //used to transfer already loaded info to the lvl scene and to get the missing values from the database
    [HideInInspector] public string lvlID;
    [HideInInspector] public string lvlName;
    [HideInInspector] public int lvlLikes;
    [HideInInspector] public string lvlJSON;
    
    public static LevelInfoHolder instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
