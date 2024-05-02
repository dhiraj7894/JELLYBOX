using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jelly.Core.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField]private Dictionary<string, Quest> questMap;

        private void Awake()
        {
            questMap = CreateQuestMap();

        }
        private void Start()
        {
            foreach (Quest quest in questMap.Values)
            {
                QuestEvent.QuestStateChange(quest);
            }
        }
        private Dictionary<string, Quest> CreateQuestMap() {
            QuestSystemSO[] allQuest = Resources.LoadAll<QuestSystemSO>("Quests");

            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestSystemSO questInfo in allQuest)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.Log($"Found a duble key with id of : {questInfo.id}");
                    
                }
                idToQuestMap.Add(questInfo.id, new Quest(questInfo));
                
            }
            return idToQuestMap;
        }

        private Quest GetQuestByID(string id)
        {
            Quest quest = questMap[id];
            if(quest == null)
            {
                Debug.Log($"ID NOT FOINT IN QUEST MAP {id}");
            }
            
            return quest;
        }
        
        void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestByID(id);
            quest.state = state;
            QuestEvent.QuestStateChange(quest);
        }

        private void OnEnable()
        {
            QuestEvent.onStartQuest += StartQuest;
            QuestEvent.onAdvanceQuest += AdvacneQuest;
            QuestEvent.onFinishQuest += FinishQuest;
        }

        private void OnDisable()
        {
            QuestEvent.onStartQuest -= StartQuest;
            QuestEvent.onAdvanceQuest -= AdvacneQuest;
            QuestEvent.onFinishQuest -= FinishQuest;
        }

        bool CheckRequirementMet(Quest quest)
        {
            bool meetRequirement = true;
            if(GameManager.Instance.playerLevel < quest.info.levelRequirement)
            {
                meetRequirement = false;
            }
            foreach (QuestSystemSO preRequirementInfo in quest.info.questRequirement)
            {
                if(GetQuestByID(preRequirementInfo.id).state != QuestState.FINISHED)
                {
                    meetRequirement = false;
                }
            }
            return meetRequirement;     
        }

        private void Update()
        {

            foreach (Quest quest in questMap.Values)
            {
                //Debug.Log($"WHY ?");
                if (quest.state == QuestState.REQUIREMENT_NOT_MET && CheckRequirementMet(quest))
                {
                    
                    ChangeQuestState(quest.info.id, QuestState.CAN_START);
                }
            }
        }


        private void StartQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            quest.SpwanCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        }
        private void AdvacneQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            quest.MoveToNextStep();
            if(quest.CurrentStepExists())
            {
                quest.SpwanCurrentQuestStep(this.transform);
            }
            else
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            }

        }
        private void FinishQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            ClaimReward(quest);
        }


        void ClaimReward(Quest quest)
        {
            //Add XP to player level
            //Add Object reward to player invetory 
            //Add In Jelly Currency as reward
            Debug.Log($"<color=green>Jelly Reward Added to you inventory</color>");
            ChangeQuestState(quest.info.id, QuestState.FINISHED);
        }
    }
}
