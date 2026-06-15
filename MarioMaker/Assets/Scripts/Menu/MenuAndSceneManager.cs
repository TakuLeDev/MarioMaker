using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAndSceneManager : MonoBehaviour
{
    [SerializeField] List<GameObject> menuItems;
    [SerializeField] GameObject buttonPrefab;
    public void Navigate(GameObject targetMenu)
    {
        foreach (GameObject menuItem in menuItems)
        {
            if(!menuItem.activeSelf) continue;
            menuItem.SetActive(false);
        }
        if(targetMenu == null) return;
        targetMenu.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        if(AccountManager.instance.UserID != "-1") SceneManager.LoadScene(sceneName);
    }

    public void InstantiateLevelButtons(GameObject targetMenu)
    {
        Transform[] childs = targetMenu.GetComponentsInChildren<Transform>();
        if (childs.Length != 0) {
            foreach (Transform button in childs) {
                if (button.gameObject != targetMenu) {
                    Destroy(button.gameObject);
                }
            }
        }
        
        StartCoroutine(AccountManager.instance.GetLvlInfos(10, (temp) =>
        {
            string wrappedJson = "{ \"levels\": " + temp + "}";
            LevelInfoList levelList = JsonUtility.FromJson<LevelInfoList>(wrappedJson);
            
            foreach (LevelInfo level in levelList.levels)
            {
                GameObject button = Instantiate(buttonPrefab, targetMenu.transform);
                
                LvlButtonData buttonData = button.GetComponent<LvlButtonData>();
                
                
                buttonData.lvlName = level.name;
                buttonData.lvlID = level.id;
                buttonData.lvlNameText.text = level.name;
                //buttonData.lvlLikesText.text = level.;
            }
        }));
    }

    public void QuitGame()
    {
        try
        {
            AccountManager.instance.StartLogin();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
        Application.Quit();
    }
}

[Serializable]
public class LevelInfo
{
    public string id;
    public string name;
    public string creation_date;
}

[Serializable]
public class LevelInfoList
{
    public List<LevelInfo> levels;
}
