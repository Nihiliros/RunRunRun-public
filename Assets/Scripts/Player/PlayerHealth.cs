using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    List<GameObject> hearts;
    int life = 6;
    bool isIFrames = false;
    UnityEvent loseLife;
    UnityEvent gameOver;
    UnityEvent addLife;
    GameObject manager;

    void Start()
    {
        SetupUI();

        //Creation of events
        if (loseLife == null)
        {
            loseLife = new UnityEvent();
        }
        loseLife.AddListener(LoseLifeEvent);

        if (addLife == null)
        {
            addLife = new UnityEvent();
        }
        addLife.AddListener(AddLifeEvent);

        if (gameOver == null)
        {
            gameOver = new UnityEvent();
        }
        gameOver.AddListener(GameOverEvent);

        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void GetDamage()
    {
        if (life > 0) 
        {
            StartCoroutine(SetIFrames());
            life--;
            SetupUI();
            loseLife.Invoke();
            if (life <= 0)
            {
                StartCoroutine(EndGame());
            }
        }
    }

    public void GetHeal()
    {
        if (life < 12)
        {
            life++;
            addLife.Invoke();
            SetupUI();
        }
    }

    void SetupUI()
    {
        for(int i=0; i < hearts.Count; i++)
        {
            if (i >= life)
            {
                hearts[i].SetActive(false);
            }
            else
            {
                hearts[i].SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isIFrames && this.gameObject.GetComponent<CharacterController>().GetDamageStatus())
        {
            GetDamage();
        }
    }

    IEnumerator SetIFrames()
    {
        isIFrames = true;
        yield return new WaitForSeconds(1f);
        isIFrames = false;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.2f);
        gameOver.Invoke();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameOver");
    }

    //Calling of events
    void LoseLifeEvent()
    {
        manager.GetComponent<AchievementManager>().FirstTimeUnlock();
        GameObject wave = GameObject.FindGameObjectWithTag("WaveManager");
        wave.GetComponent<WavePassedManager>().WaveReset();
    }
    void GameOverEvent()
    {
        manager.GetComponent<AchievementListener>().SuicideCheck();
        manager.GetComponent<AchievementListener>().InputCheck();
        manager.GetComponent<AchievementListener>().UltiCheck();
        manager.GetComponent<ScoreManagement>().GameOverScore();
    }
    void AddLifeEvent()
    {
        manager.GetComponent<AchievementListener>().AddLifeCount();
    }

    public int GetLife()
    {
        return life;
    }
}
