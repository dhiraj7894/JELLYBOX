using Game.Core.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Quest
{
    public class QuestTrigger : IActionTrigger
    {
        public QuestPoint QuestPoint;
        public override void Trigger()
        {
            Debug.Log($"Quest event trigger");
            QuestPoint.ActivateQuest();
        }
    }
}
