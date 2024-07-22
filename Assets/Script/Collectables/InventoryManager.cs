using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Jelly.Player
{
   
    public class InventoryManager : Singleton<InventoryManager>
    {
        public List<Item> items = new List<Item>();
        public ItemController itemData;
        public Transform itemTransform;

        private void Update()
        {
        }


        public void AddItem(Item item)
        {
            bool itemExists = false;
            if (items.Count > 0)
            {
                foreach (Item itemData in items)
                {                   
                    if (itemData.itemType == item.itemType)
                    {
                        itemData.count += 1;
                        Debug.Log("1");
                        itemExists = true;
                        break; // Exit the loop as we have found and updated the item
                    }
                }

                // If no existing item was found, add the new item
                if (!itemExists)
                {
                    items.Add(item);
                    item.count = 1; 
                }
            }
            else
            {
                items.Add(item);
                item.count = 1;
            }

        }
        public void RemoveItem(Item item)
        {
            bool itemExists = false;
            if (items.Count > 0)
            {
                foreach (Item itemData in items)
                {
                    if (itemData.itemType == item.itemType && itemData.count > 0)
                    {
                        itemData.count -= 1;
                        Debug.Log("1");
                        itemExists = true;
                        break; // Exit the loop as we have found and updated the item
                    }
                }

                // If no existing item was found, add the new item
                if (!itemExists)
                {
                    items.Remove(item);
                }
            }
            else
            {
                items.Remove(item);
            }

        }

        public void ManageListItem()
        {
            foreach (Transform item in itemTransform)
            {
                Destroy(item.gameObject);
            }

            foreach(Item item in items)
            {
                ItemController itemDetails = Instantiate(itemData, itemTransform);
                itemDetails.item = item;
                itemDetails.itemDetails.icon.sprite = item.icon;
                itemDetails.itemDetails.itemCount.text = item.count.ToString();                
            }
        }       
    }
}
