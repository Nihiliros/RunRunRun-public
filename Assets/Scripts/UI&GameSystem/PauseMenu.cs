using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject UIPause;
    [SerializeField]
    GameObject UIAchieve;
    [SerializeField]
    List<GameObject> UIGame;
    [SerializeField]
    UnityEvent ActualizeMenus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }

    void SetPause()
    {
        ActualizeMenus.Invoke();
        UIPause.SetActive(true);
        Time.timeScale = 0;
        foreach(GameObject ui in UIGame)
        {
            ui.SetActive(false);
        }
    }

    public void UnSetPause()
    {      
        UIPause.SetActive(false);
        foreach (GameObject ui in UIGame)
        {
            ui.SetActive(true);
        }
        Time.timeScale = 1;
    }

    public void PrintAchievements()
    {
        UIAchieve.SetActive(true);
    }

    public void QuitAchievements()
    {
        UIAchieve.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
