using Game.Core;
using UnityEngine;


namespace Game.Player
{

    public class PlayerController : IActionTrigger
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
                if (hit.transform.GetComponent<RaycastTarget>())
                {
                    if (!TargetedObject)
                    {
                        EventManager.Instance.PressFButton += hit.transform.GetComponent<IActionTrigger>().Trigger;                        
                        hit.transform.GetComponent<PressF_UI>().showInteractUI();
                        hit.transform.GetComponent<PressF_UI>().stats = transform.GetComponent<PlayerStats>();
                        TargetedObject = hit.transform;
                    }
                    
                }
                else
                {
                    if (TargetedObject)
                    {
                        EventManager.Instance.PressFButton -= TargetedObject.GetComponent<IActionTrigger>().Trigger;
                        TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                        TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                        TargetedObject = null;
                    }
                }

            }
            else
            {
                if (TargetedObject)
                {
                    EventManager.Instance.PressFButton -= TargetedObject.GetComponent<IActionTrigger>().Trigger;
                    TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                    TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                    TargetedObject = null;
                }
            }
        }
    }
}