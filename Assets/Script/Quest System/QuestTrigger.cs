using Jelly.Core.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Core.Quest
{
    public class QuestTrigger : IActionTrigger
    {
        public QuestPoint QuestPoint;
        public PressF_UI PressF_UI;
        public Collider Collider;
        private void OnEnable()
        {
            Collider = GetComponent<Collider>();
        }
        public override void Trigger()
        {                        
            if(QuestPoint != null )
            {
                QuestPoint.ActivateQuest();                
            }
        }


        public void Update()
        {
            //QuestCollisionChecking();
        }

        void QuestCollisionChecking()
        {
            switch (QuestPoint.currentQuestState)
            {
                case QuestState.REQUIREMENT_NOT_MET:
                    Collider.enabled = false;
                    break;
                case QuestState.CAN_START:
                    Collider.enabled = true;
                    break;
                case QuestState.IN_PROGRESS:
                    Collider.enabled = false;
                    break;
                case QuestState.CAN_FINISH:
                    Collider.enabled = true; 
                    break;
                case QuestState.FINISHED: 
                    Collider.enabled = false;
                    break;

            }
        }
    }
}
