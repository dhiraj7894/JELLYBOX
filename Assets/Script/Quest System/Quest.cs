using Game.Core.Quest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core.Quest
{
    public enum QuestState
    {
        REQUIREMENT_NOT_MET,
        CAN_START,
        IN_PROGRESS,
        CAN_FINISH,
        FINISHED
    }

    public class Quest
    {
        public QuestSystemSO info;
        public QuestState state;
        private int currentQuestStepIndex;

        public Quest(QuestSystemSO questInfo)
        {
            this.info = questInfo;
            this.state = QuestState.REQUIREMENT_NOT_MET;
            this.currentQuestStepIndex = 0;
        }

        public void MoveToNextStep()
        {
            currentQuestStepIndex++;
        }

        public bool CurrentStepExists()
        {
            return (currentQuestStepIndex < info.questSteps.Length);
        }

        public void SpwanCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep =  Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
                questStep.InitQuestStep(info.id);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExists())
            {
                questStepPrefab = info.questSteps[currentQuestStepIndex];
            }
            else
            {
                Debug.Log($"<color=red>No current Step presnt for Quest ID = {info.id} at location of {currentQuestStepIndex}</color>");
            }
            return questStepPrefab;
        }
    }
}