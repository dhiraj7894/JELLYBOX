using System;
using System.Diagnostics;
using UnityEngine;

namespace Jelly.Core.Quest
{
    public static class QuestEvent
    {
        public static event Action<string> onStartQuest;
        public static void StartQuest(string id)
        {
            onStartQuest?.Invoke(id);            
        }

        public static event Action<string> onAdvanceQuest;
        public static void AdvanceQuest(string id)
        {
            onAdvanceQuest?.Invoke(id);
        }
        public static event Action<string> onFinishQuest;
        public static void FinishQuest(string id)
        {
            onFinishQuest?.Invoke(id);
        }

        public static event Action<Quest> onQuestStateChange;
        public static void QuestStateChange(Quest quest)
        {
            onQuestStateChange?.Invoke(quest);
        }
    }
}
