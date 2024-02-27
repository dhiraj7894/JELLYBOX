using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagHash.PLAYER))
        {
            EventManager.OnCoinCollected();
            this.gameObject.SetActive(false);
        }
    }
}
