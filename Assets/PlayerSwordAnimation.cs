using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSwordAnimation : MonoBehaviour
    {

        public MainPlayer MainPlayer;
        public void Cooldown()
        {
            MainPlayer.isCooldown = true;
        }
    }
}

