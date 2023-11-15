using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Game.Core
{
    public class EventManager : Singleton<EventManager>
    {
        public event Action PressFButton;
    }
}
