using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlButtonData : MonoBehaviour
{
    public string lvlID;
    public string lvlName;
    public int lvlLikes;
    public TMP_Text lvlNameText;
    public TMP_Text lvlLikesText;

    public void StartLvlButtonPlayMode()
    {
        SceneManager.LoadScene("LevelPlayMode");
        LevelInfoHolder.instance.lvlID = lvlID;
        LevelInfoHolder.instance.lvlName = lvlName;
        LevelInfoHolder.instance.lvlLikes = lvlLikes;
    }
}
