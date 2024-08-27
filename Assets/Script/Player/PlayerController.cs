using Ink;
using Jelly.Core;
using Jelly.Enemy;
using Unity.VisualScripting;
using UnityEngine;


namespace Jelly.Player
{

    public class PlayerController : IActionTrigger
    {        
        RaycastHit hit;
        public LayerMask ditectionLayer;

        public Camera cam;
        public Transform TargetedObject;
        public PlayerStats playerStats;
        public bool isPlayerNear = false;

        public float Distance = 3;
        private void Update()
        {
            if (isPlayerNear)
                return;
                
                rycaster();
        }

        void rycaster()
        {
            if(Physics.Raycast(cam.transform.position,cam.transform.forward, out hit, Distance, ditectionLayer))
            {
                if (hit.transform.GetComponent<RaycastTarget>())
                {
                    if (!TargetedObject)
                    {
                        EventManager.Instance.PressFButton += hit.transform.GetComponent<IActionTrigger>().Trigger;                        
                        hit.transform.GetComponent<PressF_UI>().stats = playerStats;
                        hit.transform.GetComponent<PressF_UI>().showInteractUI();
                        TargetedObject = hit.transform;
                    }
                    
                }
                else
                {
                    if (TargetedObject)
                    {
                        EventManager.Instance.PressFButton -= TargetedObject.GetComponent<IActionTrigger>().Trigger;
                        TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                        TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                        TargetedObject = null;
                    }
                }

            }
            else
            {
                if (TargetedObject)
                {
                    EventManager.Instance.PressFButton -= TargetedObject.GetComponent<IActionTrigger>().Trigger;
                    TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                    TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                    TargetedObject = null;
                }
            }
        }



        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<RaycastTarget>())
            {                
                if (!TargetedObject)
                {
                    EventManager.Instance.PressFButton += other.transform.GetComponent<IActionTrigger>().Trigger;
                    other.transform.GetComponent<PressF_UI>().showInteractUI();
                    other.transform.GetComponent<PressF_UI>().stats = transform.GetComponent<PlayerStats>();
                    TargetedObject = other.transform;
                }
                isPlayerNear = true;
            }
            /*else
            {
                if (TargetedObject)
                {
                    EventManager.Instance.PressFButton -= TargetedObject.GetComponent<IActionTrigger>().Trigger;
                    TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                    TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                    TargetedObject = null;
                }
            }*/

        }

        private void OnTriggerExit(Collider other)
        {
            if (TargetedObject)
            {
                isPlayerNear = false;
                EventManager.Instance.PressFButton -= TargetedObject.GetComponent<IActionTrigger>().Trigger;
                TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                TargetedObject = null;
                isPlayerNear = false;
            }
        }
    }
}