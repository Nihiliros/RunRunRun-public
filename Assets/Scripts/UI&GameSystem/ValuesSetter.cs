using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValuesSetter : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Toggle isGet;

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    } 

    public void SetTitle(string titleText)
    {
        title.text = titleText;
    }

    public void SetText(string textText)
    {
        text.text = textText;
    }

    public void SetIsGet(bool tick)
    {
        if (tick)
        {
            isGet.isOn = true;
        }
        else
        {
            isGet.isOn = false;
        }       
    }
}
