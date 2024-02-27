using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            this.gameObject.SetActive(false);
        }
    }
}
