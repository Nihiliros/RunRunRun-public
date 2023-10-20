using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Contains the conditions & values when this unlocking conditions of achievements are not spontaneous.
public class AchievementListener : MonoBehaviour
{
    [SerializeField]
    UnityEvent DogeUnlock;
    [SerializeField]
    UnityEvent SpamUnlock;
    [SerializeField]
    UnityEvent UltiUnlock;
    int spamCounter = 0;
    int ultiCounter = 0;
    int maxLife=0;
    int suicidedLife=0;
    UnityEvent GetLife;
    [SerializeField]
    UnityEvent SuicideUnlock;
    bool hasInput = false;
    [SerializeField]
    UnityEvent AFKUnlock;

    private void Start()
    {
        StartCoroutine(TimerDoge());
        if (GetLife == null)
        {
            GetLife = new UnityEvent();
        }
        GetLife.AddListener(GetLifeCount);
        GetLife.Invoke();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            hasInput = true;
        }
    }

    IEnumerator TimerDoge()
    {
        yield return new WaitForSeconds(10);
        DogeUnlock.Invoke();
    }

    public void SpamAdd()
    {
        spamCounter++;
        if (spamCounter == 100)
        {
            SpamUnlock.Invoke();
        }
    }

    public void UltiAdd()
    {
        ultiCounter++;
    }

    public void UltiCheck()
    {
        if (ultiCounter == 0)
        {
            UltiUnlock.Invoke();
        }
    }

    void GetLifeCount()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        maxLife = player.GetComponent<PlayerHealth>().GetLife();
    }

    public void AddLifeCount()
    {
        maxLife++;
    }

    public void AddSuicideCount()
    {
        suicidedLife++;
    }

    public void SuicideCheck()
    {
        if (suicidedLife == maxLife)
        {
            SuicideUnlock.Invoke();
        }
    }

    public void InputCheck()
    {
        if (!hasInput)
        {
            AFKUnlock.Invoke();
        }
    }
}
