using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public abstract class EBase : MonoBehaviour
    {
        public abstract EBase Tick(EManager manager, EStats stats, EAnimeManager anim);
    }
}
