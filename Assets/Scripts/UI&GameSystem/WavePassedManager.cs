using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Used on a game object. Reader of waves of enemies, for the unlocking of 2 achievements
public class WavePassedManager : MonoBehaviour
{
    bool isNextWave = true;
    int wavePassed = 0;
    UnityEvent WaveCompleted;

    private void Start()
    {
        if (WaveCompleted == null)
        {
            WaveCompleted = new UnityEvent();
        }
        WaveCompleted.AddListener(WavesEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isNextWave)
        {
            isNextWave = false;
            wavePassed++;
            WaveCompleted.Invoke();
            StartCoroutine(TimerWave());
        }
    }

    IEnumerator TimerWave()
    {
        yield return new WaitForSeconds(0.2f);
        isNextWave = true;
    }

    public void WaveReset()
    {
        wavePassed = 0;
    }

    void WavesEvent()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (wavePassed == 5 && player.GetComponent<CharacterController>().GetForm() == "Bunny")
        {
            GameObject manager = GameObject.FindGameObjectWithTag("Manager");
            manager.GetComponent<AchievementManager>().BugsUnlock();
        }
        if (wavePassed == 10 && player.GetComponent<CharacterController>().GetForm() == "Evade")
        {
            GameObject manager = GameObject.FindGameObjectWithTag("Manager");
            manager.GetComponent<AchievementManager>().FlashUnlock();
        }
    }
}
