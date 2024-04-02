using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class AttackP2 : EBase
    {
        public ECombat combat;
        public bool isAttacking;
        public bool isAttackCompleted;
        public override EBase Tick(EManager manager, EStats stats, EAnimeManager anim)
        {



            if(isAttacking)
            {
                return null;
            }else if(isAttackCompleted)
            {
                return combat;
            }
            return combat;
        }
    }
}
