using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Score/Score")]
public class ScoreSave : ScriptableObject
{
    int finalScore;
    
    public void SetScore(int score)
    {
        finalScore = score;
    }

    public int GetScore()
    {
        return finalScore;
    }
}
