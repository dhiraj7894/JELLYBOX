using Jelly.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jelly
{
    [Serializable]
    public class ItemDetails
    {
        public Image icon;
        public TextMeshProUGUI itemCount;
    }
    public class ItemController : MonoBehaviour
    {
        public Item item;
        public ItemDetails itemDetails;
        public Collider col;
        private GameObject Player;
        private void Start()
        {
            Player = FindObjectOfType<MainPlayer>().gameObject;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TagHash.PLAYER))
            {
                Destroy(gameObject);
                if (col.enabled)
                {
                    Debug.Log($"Object type : {item.itemType} & {item.name}");
                    InventoryManager.Instance.AddItem(item);
                    col.enabled = false;
                }
                
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            LeanTween.move(gameObject, Player.transform, .2f);
        }
        public void UseItem()
        {
            if (item)
            {
                InventoryManager.Instance.RemoveItem(item);
                itemDetails.itemCount.text = item.count.ToString();
                if (item.count == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
