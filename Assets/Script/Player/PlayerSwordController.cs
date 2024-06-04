using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jelly.Player
{
    public class PlayerSwordController : MonoBehaviour
    {
        public Collider col;

        public void EnableCollider()
        {
            col.enabled = true;
        }
        public void DesableCollider()
        {
            col.enabled = false;
        }
    }
}