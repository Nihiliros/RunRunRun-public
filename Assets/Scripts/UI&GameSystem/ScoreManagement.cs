using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagement : MonoBehaviour
{
    int score;
    [SerializeField]
    TextMeshProUGUI UIScore;

    [SerializeField]
    ScoreSave save;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        UIScore.text = "Score: "+score;
    }

    public void AddScore(int scoreGet)
    {
        score += scoreGet;
    }

    public void GameOverScore()
    {
        save.SetScore(score);
    }
}
