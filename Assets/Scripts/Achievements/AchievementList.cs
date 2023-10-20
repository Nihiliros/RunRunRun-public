using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementList", menuName = "Achievement/Achievement List")]
public class AchievementList : ScriptableObject
{
    public List<Achievment> achievements;

    public Achievment GetAchievement(int nbAchievement)
    {
        return achievements[nbAchievement];
    }

    public bool GetIsUnlocked(int nbAchievement)
    {
        return achievements[nbAchievement].isUnlocked;
    }

    public void SetIsUnlocked(int nbAchievement)
    {
        achievements[nbAchievement].isUnlocked = true;
    }
}
