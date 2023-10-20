using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject UIAchieve;

    [SerializeField]
    List<GameObject> UIMenu;

    public void PrintAchievements()
    {
        UIAchieve.SetActive(true);
        foreach(GameObject menu in UIMenu)
        {
            menu.SetActive(false);
        }
    }

    public void QuitAchievements()
    {
        foreach (GameObject menu in UIMenu)
        {
            menu.SetActive(true);
        }
        UIAchieve.SetActive(false);
    }

    public void Game()
    {
        SceneManager.LoadScene("Level");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
