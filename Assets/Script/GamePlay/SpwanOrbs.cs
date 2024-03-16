using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game.Core
{
    public class SpwanOrbs : Singleton<SpwanOrbs>
    {
        public int orbsCount = 0;

        private void OnEnable()
        {
            EventManager.OrbCollected += OrbCollection;
        }
        private void OnDisable()
        {
            EventManager.OrbCollected -= OrbCollection;
        }

        public void OrbCollection()
        {
            orbsCount++;
            UIManager.Instance.CoinCount.text = orbsCount.ToString();
        }
    }
}

