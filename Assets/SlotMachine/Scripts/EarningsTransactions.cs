using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EarningsTransactions : MonoBehaviour
{
    public static EarningsTransactions Instance;

    //Money burda Oyundaki Total paramız
    public long Money;
    public int TotalMoney;
    public int GlobalMoney;

    public bool ActiveMiniGame;

    public Text BetText;

    private void Start() => Instance = this;

    public void QuantityRatio(int quantity)
    {
        if (ActiveMiniGame || quantity > Money) return;
        Money -= quantity;
        TotalMoney += quantity;
        BetText.text = TotalMoney.ToString(); //strinextions kodu buraya gelicek
    }

    public bool StartGameControl()
    {
        ActiveMiniGame = TotalMoney != 0;
        return ActiveMiniGame;
    }

    public void WinnerEvent(float multi)
    {
        Money += (int)(TotalMoney * multi);
        Debug.Log("Kazandın : " + (int)(TotalMoney * multi));
        DefaultEvent();
    }

    public void DefaultEvent()
    {
        ActiveMiniGame = false;
        BetText.text = (TotalMoney -= TotalMoney).ToString();
        GlobalMoney = 0;
    }
}