using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PressF_UI : MonoBehaviour
    {
        public PlayerStats stats;
        public CanvasGroup CanvasGroup;
        public LookAtCamera LAC;
        public RaycastTarget RaycastTarget;

        private void Awake()
        {
            RaycastTarget = GetComponent<RaycastTarget>();
        }

        public void showInteractUI()
        {
            if (RaycastTarget == null)
                return;

            LeanTween.value(this.gameObject, 0, 1, 0.1f).setOnUpdate((float val) => { CanvasGroup.alpha = val; });
            LAC.LookAtCam();
        }
        public void hideInteractUI()
        {
            if (RaycastTarget == null)
                return;
            LeanTween.value(this.gameObject, 1, 0, 0.1f).setOnUpdate((float val) => { CanvasGroup.alpha = val; });
            LAC.LookAtCam();

        }
    }
}
