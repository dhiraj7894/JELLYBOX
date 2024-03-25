using Jelly.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrbs : QuestStep
{
    private int OrbsCollect = 0;
    private int totalCoinsCollect = 5;
    public string ObjectName;

    private void OnEnable()
    {
        EventManager.OrbCollected += OrbCollected;
    }
    private void OnDisable()
    {
        EventManager.OrbCollected -= OrbCollected;
    }

    public void OrbCollected()
    {
        OrbsCollect++;
        if(OrbsCollect >= totalCoinsCollect)
            FinishedQuestStep();
    }
}
