using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Quest
{
    public class QuestPoint : MonoBehaviour
    {
        private bool isPlayerNear = false;

        private PressF_UI pressF_UI;


        [Header("Quest")]
        [SerializeField] private QuestSystemSO questInfoForPoint;
        private string questId;
        public QuestState currentQuestState;
        public QuestIcon QuestIcon;
        public bool isStartPoint = false;
        public bool isFinishPoint = false;

        [Header("Quest Releted Item")]
        public List<GameObject> QuestItem = new List<GameObject>();


        private void Awake()
        {
            questId = questInfoForPoint.id;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagHash.PLAYER))
            {
                isPlayerNear = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(TagHash.PLAYER) && currentQuestState == QuestState.IN_PROGRESS && pressF_UI)
            {
                pressF_UI.CanvasGroup.gameObject.SetActive(false);
            }
            else
            {
                pressF_UI.CanvasGroup.gameObject.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagHash.PLAYER))
            {
                isPlayerNear = false;
            }
        }

        private void OnEnable()
        {
            pressF_UI = GetComponent<PressF_UI>();
            QuestItemData();
            QuestEvent.onQuestStateChange += QuestStateChange;
            //EventManager.Instance.PressFButton += ActivateQuest;
        }
        private void OnDisable()
        {
            QuestEvent.onQuestStateChange -= QuestStateChange;
            //EventManager.Instance.PressFButton -= ActivateQuest;
        }

        public void QuestStateChange(Quest quest)
        {
            if (quest.info.id.Equals(questId))
            {
                currentQuestState = quest.state;
                QuestIcon.SetState(currentQuestState, isStartPoint, isFinishPoint);
            }
        }

        public void ActivateQuest()
        {
            if (!isPlayerNear)
            {
                return;
            }

            if (currentQuestState.Equals(QuestState.CAN_START) && isStartPoint)
            {
                
                UIManager.Instance.CutSceneFadeOutIn(0.5f);
                LeanTween.delayedCall(.4f, () => { 
                    QuestEvent.StartQuest(questId);
                    QuestItemData(true);
                });
                
            }
            if (currentQuestState.Equals(QuestState.CAN_FINISH) && isFinishPoint)
            {
                QuestItemData();
                UIManager.Instance.CutSceneFadeOutIn(0.5f);
                LeanTween.delayedCall(.4f, () => {
                    QuestEvent.FinishQuest(questId);
                });
                
            }

        }

        public void QuestItemData(bool isTrue=false)
        {
            if (QuestItem.Count > 0)
            {
                foreach (GameObject item in QuestItem)
                {
                    item.SetActive(isTrue);
                }
            }
        }

    }
}
