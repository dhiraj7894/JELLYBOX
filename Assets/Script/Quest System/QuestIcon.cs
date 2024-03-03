using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Quest
{
    public class QuestIcon : MonoBehaviour
    {
        [SerializeField] private GameObject requirementNotMetGameObject;
        [SerializeField] private GameObject canStartGameObject;
        [SerializeField] private GameObject inProgressGameObject;
        [SerializeField] private GameObject canFinishGameObject;
        [Header("Raycast Detection")]
        public GameObject RaycastTarget;



        public void SetState(QuestState state, bool startPoint, bool finishPoint)
        {
            requirementNotMetGameObject.SetActive(false);
            canStartGameObject.SetActive(false);
            inProgressGameObject.SetActive(false);
            canFinishGameObject.SetActive(false);

            switch (state)
            {
                case QuestState.REQUIREMENT_NOT_MET:
                    if(startPoint) requirementNotMetGameObject.SetActive(true);
                    break;
                case QuestState.CAN_START:
                    if (startPoint) canStartGameObject.SetActive(true);
                    break;
                case QuestState.IN_PROGRESS:
                    if (finishPoint) inProgressGameObject.SetActive(true);
                    break;
                case QuestState.CAN_FINISH:
                    if (finishPoint) { 
                        canFinishGameObject.SetActive(true);                        
                    }
                    break;
                case QuestState.FINISHED:
                    Destroy(RaycastTarget.GetComponent<RaycastTarget>());
                    break;
                default:
                    Debug.Log($"{state}");
                    break;

            }
        }
    }
}
