using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class HunterMinion : MinionBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagHash.PlayerSword))
            {
                isDead = true;
                EnemyManager.Instance.UpdateMinionsDeath();
            }
        }
    }
}
