using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
public class Meta : MonoBehaviour
{
    public UnityAction OnAllCardInfoLoad;

    private UI_playerCardVC ui_playerCardVC;
    
    public int stars;
    public int crystal;
    
    public TextAsset allCardsInfoAsset;
    public AllCardsInfo allCardsInfo;
    
    public int activeDeck;
    
    public TextAsset playerCardsAsset;
    public PlayerCards playerCards;

    public void Start()
    {
        ui_playerCardVC = GetComponent<UI_playerCardVC>();
        ui_playerCardVC.Start();
        
        allCardInfoLoad();
        playerCardsLoad();
        
        OnAllCardInfoLoad?.Invoke();
    }

    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            string text = JsonUtility.ToJson(playerCards);
            Debug.Log(text);
        }*/
    }

    private void allCardInfoLoad()
    {
        allCardsInfo = JsonUtility.FromJson<AllCardsInfo>(allCardsInfoAsset.text);
    }
    private void playerCardsLoad()
    {
        playerCards = JsonUtility.FromJson<PlayerCards>(playerCardsAsset.text);
    }
}


