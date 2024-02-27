using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Core.Quest
{
    public class QuestManager : MonoBehaviour
    {
        private Dictionary<string, Quest> questMap;

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



        private void StartQuest(string id)
        {

        }
        private void AdvacneQuest(string id)
        {

        }
        private void FinishQuest(string id)
        {

        }
    }
}
