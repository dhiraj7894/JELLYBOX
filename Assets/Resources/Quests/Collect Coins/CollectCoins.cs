using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : QuestStep
{
    private int CoinCollect = 0;
    private int totalCoinsCollect = 5;

    private void OnEnable()
    {
        EventManager.CoinCollected += CoinCollected;
    }
    private void OnDisable()
    {
        EventManager.CoinCollected -= CoinCollected;
    }

    public void CoinCollected()
    {
        CoinCollect++;
    }
}
