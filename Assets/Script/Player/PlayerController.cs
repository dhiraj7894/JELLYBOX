using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Game.Player
{

    public class PlayerController : MonoBehaviour
    {        
        RaycastHit hit;
        public Camera cam;
        public Transform TargetedObject;

        public float Distance = 3;
        private void Update()
        {
            rycaster();
        }

        void rycaster()
        {
            if(Physics.Raycast(cam.transform.position,cam.transform.forward, out hit, Distance))
            {
                if (hit.transform.GetComponent<ReycastTarget>())
                {
                    if (!TargetedObject)
                    {
                        hit.transform.GetComponent<HealingFontain>().showInteractUI();
                        hit.transform.GetComponent<HealingFontain>().playerStats = transform.GetComponent<PlayerStats>();
                        TargetedObject = hit.transform;
                    }
                    
                }
                else
                {
                    if (TargetedObject)
                    {
                        TargetedObject.GetComponent<HealingFontain>().hideInteractUI();
                        TargetedObject.transform.GetComponent<HealingFontain>().playerStats = null;
                        TargetedObject = null;
                    }
                }

            }
            else
            {
                if (TargetedObject)
                {
                    TargetedObject.GetComponent<HealingFontain>().hideInteractUI();
                    TargetedObject.transform.GetComponent<HealingFontain>().playerStats = null;
                    TargetedObject = null;
                }
            }
        }
    }
}