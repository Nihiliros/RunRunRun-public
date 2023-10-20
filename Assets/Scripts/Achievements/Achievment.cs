using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Achievement", menuName ="Achievement/Achievement")]
public class Achievment : ScriptableObject
{
    public Sprite image;
    public string title;
    public string description;
    public bool isUnlocked;

    public void GetAchievement()
    {
        isUnlocked = true;
    }
}
