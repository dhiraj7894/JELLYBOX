using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly {
    public class ChestAnimationTrigger : MonoBehaviour
    {
        public Chest Chest;

        public void SpwanItem()
        {
            StartCoroutine(Chest.SpawnObjects());
        }
    }

}