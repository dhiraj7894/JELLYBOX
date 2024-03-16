using Game.Core.Quest;
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
        public LookAtCamera LACamera;
        public RaycastTarget RaycastTarget;
        public QuestPoint questPoint;

        private void Awake()
        {
            RaycastTarget = GetComponent<RaycastTarget>();
        }

        public void showInteractUI()
        {
            if (RaycastTarget == null)
                return;
            if (questPoint)
            {
                if (questPoint && questPoint.currentQuestState == QuestState.IN_PROGRESS)
                    CanvasGroup.alpha = 0;
                else
                    LeanTween.value(this.gameObject, 0, 1, 0.1f).setOnUpdate((float val) => { CanvasGroup.alpha = val; });
            }
            else
            {
                LeanTween.value(this.gameObject, 0, 1, 0.1f).setOnUpdate((float val) => { CanvasGroup.alpha = val; });
            }


            LACamera.LookAtCam();
        }
        public void hideInteractUI()
        {
            if (RaycastTarget == null)
                return;
            
            LeanTween.value(this.gameObject, 1, 0, 0.1f).setOnUpdate((float val) => { CanvasGroup.alpha = val; });
            LACamera.LookAtCam();

        }
    }
}
