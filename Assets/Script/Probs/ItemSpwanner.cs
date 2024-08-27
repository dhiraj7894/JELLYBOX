using Jelly;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpwanner : MonoBehaviour
{
    public ItemController item;
    public Transform itemSpwanPosition;
    public float itemThrowForce;
    public bool isSpwanned = false;
    public void SpawnAndThrowObject()
    {
        if (!item)
            return;
        if (!isSpwanned)
        {
            GameObject spawnedObject = Instantiate(item.gameObject, itemSpwanPosition.position, Quaternion.identity);
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    0,
                    3,
                    0
                ).normalized;

                rb.AddForce(randomDirection * itemThrowForce, ForceMode.Impulse);
            }
            isSpwanned = true;
        }
              
    }
    }
