using Game.Core;
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
                        EventManager.Instance.PressFButton += hit.transform.GetComponent<HealingFontain>().HealDamage;                        
                        hit.transform.GetComponent<PressF_UI>().showInteractUI();
                        hit.transform.GetComponent<PressF_UI>().stats = transform.GetComponent<PlayerStats>();
                        TargetedObject = hit.transform;
                    }
                    
                }
                else
                {
                    if (TargetedObject)
                    {
                        EventManager.Instance.PressFButton -= TargetedObject.GetComponent<HealingFontain>().HealDamage;
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
                    EventManager.Instance.PressFButton -= TargetedObject.GetComponent<HealingFontain>().HealDamage;
                    TargetedObject.GetComponent<PressF_UI>().hideInteractUI();
                    TargetedObject.transform.GetComponent<PressF_UI>().stats = null;
                    TargetedObject = null;
                }
            }
        }
    }
}