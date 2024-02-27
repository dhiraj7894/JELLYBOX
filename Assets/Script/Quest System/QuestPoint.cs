using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Quest
{
    public class QuestPoint : MonoBehaviour
    {
        private bool isPlayerNear = false;
        [Header("Quest")]
        [SerializeField] private QuestSystemSO questInfoForPoint;
        private string questId;
        private QuestState currentQuestState;
        public QuestIcon QuestIcon;
        public bool isStartPoint = false;
        public bool isFinishPoint = false;



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
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagHash.PLAYER))
            {
                isPlayerNear = false;
            }
        }

        private void OnEnable()
        {
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
                QuestEvent.StartQuest(questId);
            }
            if (currentQuestState.Equals(QuestState.CAN_FINISH) && isFinishPoint)
            {
                QuestEvent.FinishQuest(questId);
            }

        }

    }
}
