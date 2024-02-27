using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game.Core
{
    public class SpwanCoin : Singleton<SpwanCoin>
    {
        public int coinCount = 0;

        private void OnEnable()
        {
            EventManager.CoinCollected += CoinCollection;
        }
        private void OnDisable()
        {
            EventManager.CoinCollected -= CoinCollection;
        }

        public void CoinCollection()
        {
            coinCount++;
            UIManager.Instance.CoinCount.text = coinCount.ToString();
        }
    }
}

