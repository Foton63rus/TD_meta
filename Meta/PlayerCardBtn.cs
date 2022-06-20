using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCardBtn : MonoBehaviour
{
    public int cardID;

    private Image cardImage;
    private Button button;

    public void Init(int cardID, string imgSource)
    {
        cardImage = GetComponent<Image>();
        button = GetComponent<Button>();

        this.cardID = cardID;
        Sprite sprite = Resources.Load<Sprite>(imgSource);
        cardImage.sprite = sprite;
        button.onClick.AddListener( OnClick );
    }
    public void OnClick()
    {
        Debug.Log($"Click id:{cardID}");
    }
}
