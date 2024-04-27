using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Core.MP
{
    public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
    {
        public static NetworkPlayer local;

        public void PlayerLeft(PlayerRef player)
        {
            if(player == Object.InputAuthority)
            {
                Runner.Despawn(Object);
            }
        }

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
            {
                local = this;
                Debug.Log($"Player Spwanned {null}");
            }
            else
            {
                Debug.Log($"Remote plyaer spwanned {null}");
            }
        }
    }
}