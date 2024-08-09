using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly
{
    public class Chest : IActionTrigger
    {
        public List<ItemController> itemList = new List<ItemController>();
        public Transform itemSpwanPosition;
        public Animator checstAnime;
        public float itemThrowForce = 10;
        public float spawnInterval = .1f;

        private void Start()
        {
            
        }
        public IEnumerator SpawnObjects()
        {
            foreach (ItemController obj in itemList)
            {
                SpawnAndThrowObject(obj.gameObject);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        private void SpawnAndThrowObject(GameObject obj)
        {
            GameObject spawnedObject = Instantiate(obj, itemSpwanPosition.position, Quaternion.identity);
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(1f, 1f),
                    Random.Range(-1f, 1f)
                ).normalized;

                rb.AddForce(randomDirection * itemThrowForce, ForceMode.Impulse);
            }
        }

        public override void Trigger()
        {
            //Play Animation of opening chest and then spwan the items            
            checstAnime.Play("Open");
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
