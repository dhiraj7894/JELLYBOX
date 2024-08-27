using Jelly.Core;
using Jelly.Core.Quest;
using Jelly.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly
{
    public class PressF_UI : MonoBehaviour
    {
        public PlayerStats stats;
        public RaycastTarget RaycastTarget;
        public Sprite itemIcon;

        private void Awake()
        {
            RaycastTarget = GetComponent<RaycastTarget>();
        }

        public void showInteractUI()
        {
            if (RaycastTarget == null)
                return;
            LeanTween.value(this.gameObject, 0, 1, 0.1f).setOnUpdate((float val) => { UIManager.Instance.interactionUI.alpha = val; });
            UIManager.Instance.itemUI.sprite = itemIcon;
           // LACamera.LookAtCam();
        }
        public void hideInteractUI()
        {
            if (RaycastTarget == null)
                return;
            
            LeanTween.value(this.gameObject, 1, 0, 0.1f).setOnUpdate((float val) => { UIManager.Instance.interactionUI.alpha = val; });
            //UIManager.Instance.itemUI.sprite = null;
            //LACamera.LookAtCam();

        }
    }
}
