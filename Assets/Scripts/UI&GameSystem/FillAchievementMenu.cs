using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAchievementMenu : MonoBehaviour
{
    [SerializeField]
    AchievementList list;
    [SerializeField]
    GameObject ScrollBar;
    [SerializeField]
    GameObject Element;

    void Start()
    {
        foreach(Achievment element in list.achievements)
        {
            GameObject listElement = Instantiate(Element, ScrollBar.transform);
            listElement.GetComponent<ValuesSetter>().SetImage(element.image);
            listElement.GetComponent<ValuesSetter>().SetTitle(element.title);
            listElement.GetComponent<ValuesSetter>().SetText(element.description);
            listElement.GetComponent<ValuesSetter>().SetIsGet(element.isUnlocked);
        }
    }

    public void SetIsGetActualize()
    {
        for(int i = 0; i < ScrollBar.transform.childCount; i++)
        {
            ScrollBar.transform.GetChild(i).GetComponent<ValuesSetter>().SetIsGet(list.achievements[i].isUnlocked);
        }
    }
}
