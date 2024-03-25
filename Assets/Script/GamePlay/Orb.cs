using Jelly.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(TagHash.PLAYER))
        {
            EventManager.OnOrbCollected();
            this.gameObject.SetActive(false);
        }
    }
}
