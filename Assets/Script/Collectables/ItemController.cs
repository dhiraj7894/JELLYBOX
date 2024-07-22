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
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TagHash.PLAYER))
            {
                Destroy(gameObject);
                Debug.Log(item.name + "Collision");
                InventoryManager.Instance.AddItem(item);
            }
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
