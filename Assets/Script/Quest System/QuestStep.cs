using Jelly.Core.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    public void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            QuestEvent.AdvanceQuest(questId);
            this.gameObject.SetActive(false);
        }
    }

    public void InitQuestStep(string questId)
    {
        this.questId = questId;

    }

}
