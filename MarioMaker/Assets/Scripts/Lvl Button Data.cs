using System;
using TMPro;
using UnityEngine;

public class LvlButtonData : MonoBehaviour
{
    [HideInInspector] public string lvlName;
    [HideInInspector] public int lvlLikes;
    [SerializeField] TMP_Text lvlNameText;
    [SerializeField] TMP_Text lvlLikesText;
    
    private void OnEnable()
    {
        lvlNameText.text = lvlName;
        lvlLikesText.text = lvlLikes.ToString();
    }
}
