using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public abstract class E_Base
    {
        public MainEnemy enemy;

        public E_Base(MainEnemy M_enemy)
        {
            enemy = M_enemy;
        }

        //

    }
}