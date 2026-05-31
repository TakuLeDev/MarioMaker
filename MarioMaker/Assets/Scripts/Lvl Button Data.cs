using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlButtonData : MonoBehaviour
{
    [HideInInspector] public int lvlID;
    [HideInInspector] public string lvlName;
    [HideInInspector] public int lvlLikes;
    [SerializeField] TMP_Text lvlNameText;
    [SerializeField] TMP_Text lvlLikesText;
    
    private void OnEnable()
    {
        lvlNameText.text = lvlName;
        lvlLikesText.text = lvlLikes.ToString();
    }

    public void StartLvlButtonPlayMode()
    {
        SceneManager.LoadScene("LevelPlayMode");
        LevelInfoHolder.instance.lvlID = lvlID;
        LevelInfoHolder.instance.lvlName = lvlName;
        LevelInfoHolder.instance.lvlLikes = lvlLikes;
    }
}
