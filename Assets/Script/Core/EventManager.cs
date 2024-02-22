using System;

namespace Game.Core
{
    public class EventManager : Singleton<EventManager>
    {
        public event Action PressFButton;

        public void PressedFButton()
        {
            PressFButton?.Invoke();
        }
    }
}
