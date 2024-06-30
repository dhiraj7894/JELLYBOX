using Jelly.Core.Quest;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jelly.Enemy
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public MinionBase[] minions;
        public GameObject[] targetQuests;

        public void UpdateMinionsDeath()
        {
            if (minions.All(MinionBase => MinionBase.isDead))
            {
                Debug.Log("All are dead");
                foreach (GameObject minion in targetQuests)
                {
                    if(!minion.activeSelf)
                    {
                        minion.SetActive(true);
                    }
                    if(minion.TryGetComponent<QuestTrigger>(out QuestTrigger quest))
                    {
                        quest.isAutoStart = true;
                    }
                }
            }
        }

    }
}
