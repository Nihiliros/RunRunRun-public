using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    [SerializeField]
    ScoreSave score;
    [SerializeField]
    TextMeshProUGUI ScoreText;

    private void Start()
    {
        SetScore();
    }

    public void SetScore()
    {
        ScoreText.text = "Final Score: " + score.GetScore();
    }
}
