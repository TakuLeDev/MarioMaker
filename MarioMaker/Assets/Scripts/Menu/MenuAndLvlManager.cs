using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAndLvlManager : MonoBehaviour
{
    [SerializeField] List<GameObject> menuItems;
    public void Navigate(GameObject targetMenu)
    {
        foreach (GameObject menuItem in menuItems)
        {
            if(!menuItem.activeSelf) continue;
            menuItem.SetActive(false);
        }
        targetMenu.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
