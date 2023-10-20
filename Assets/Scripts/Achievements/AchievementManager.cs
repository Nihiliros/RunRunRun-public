using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Manager of all Achievements. Unlocks them when functions are called
public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    AchievementList AchievementList;
    [SerializeField]
    Image popUpImage;
    [SerializeField]
    TextMeshProUGUI popUpTitle;
    [SerializeField]
    GameObject popUp;

    public void FirstTimeUnlock()
    {
        if (!AchievementList.GetIsUnlocked(0))
        {
            AchievementList.SetIsUnlocked(0);
            PopUp(0);
        }
    }
    public void RickRollUnlock()
    {
        if (!AchievementList.GetIsUnlocked(1))
        {
            AchievementList.SetIsUnlocked(1);
            PopUp(1);
        }
    }
    public void DogeUnlock()
    {
        if (!AchievementList.GetIsUnlocked(2))
        {
            AchievementList.SetIsUnlocked(2);
            PopUp(2);
        }
    }
    public void FlashUnlock()
    {
        if (!AchievementList.GetIsUnlocked(3))
        {
            AchievementList.SetIsUnlocked(3);
            PopUp(3);
        }
    }
    public void SpamUnlock()
    {
        if (!AchievementList.GetIsUnlocked(4))
        {
            AchievementList.SetIsUnlocked(4);
            PopUp(4);
        }
    }
    public void BugsUnlock()
    {
        if (!AchievementList.GetIsUnlocked(5))
        {
            AchievementList.SetIsUnlocked(5);
            PopUp(5);
        }
    }
    public void EliatropUnlock()
    {
        if (!AchievementList.GetIsUnlocked(6))
        {
            AchievementList.SetIsUnlocked(6);
            PopUp(6);
        }
    }
    public void QueenUnlock()
    {
        if (!AchievementList.GetIsUnlocked(7))
        {
            AchievementList.SetIsUnlocked(7);
            PopUp(7);
        }
    }
    public void AppleUnlock()
    {
        if (!AchievementList.GetIsUnlocked(8))
        {
            AchievementList.SetIsUnlocked(8);
            PopUp(8);
        }
    }
    public void BrianUnlock()
    {
        if (!AchievementList.GetIsUnlocked(9))
        {
            AchievementList.SetIsUnlocked(9);
            PopUp(9);
        }
    }
    public void MegamindUnlock()
    {
        if (!AchievementList.GetIsUnlocked(10))
        {
            AchievementList.SetIsUnlocked(10);
            PopUp(10);
        }
    }
    public void UltimateUnlock()
    {
        if (!AchievementList.GetIsUnlocked(11))
        {
            AchievementList.SetIsUnlocked(11);
            PopUp(11);
        }
    }
    public void SuicideUnlock()
    {
        if (!AchievementList.GetIsUnlocked(12))
        {
            AchievementList.SetIsUnlocked(12);
            PopUp(12);
        }
    }
    public void AFKUnlock()
    {
        if (!AchievementList.GetIsUnlocked(13))
        {
            AchievementList.SetIsUnlocked(13);
            PopUp(13);
        }
    }
    public void WorthUnlock()
    {
        if (!AchievementList.GetIsUnlocked(14))
        {
            AchievementList.SetIsUnlocked(14);
            PopUp(14);
        }
    }
    public void FineUnlock()
    {
        if (!AchievementList.GetIsUnlocked(15))
        {
            AchievementList.SetIsUnlocked(15);
            PopUp(15);
        }
    }
    public void UGoodUnlock()
    {
        if (!AchievementList.GetIsUnlocked(16))
        {
            AchievementList.SetIsUnlocked(16);
            PopUp(16);
        }
    }
    public void WhatUnlock()
    {
        if (!AchievementList.GetIsUnlocked(17))
        {
            AchievementList.SetIsUnlocked(17);
            PopUp(17);
        }
    }
    
    //Creates a pop up on the screen when an achievement is unlocked
    void PopUp(int nb)
    {
        popUpImage.sprite = AchievementList.achievements[nb].image;
        popUpTitle.text = AchievementList.achievements[nb].title;
        StopCoroutine(PopUpTimer());
        StartCoroutine(PopUpTimer());
    }

    IEnumerator PopUpTimer()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(2);
        popUp.SetActive(false);
    }
}
