using System;

namespace Game.Core
{
    public class EventManager : Singleton<EventManager>
    {
        public event Action PressFButton;
        public static event Action SpecialAttackEnd;
        public static event Action CoinCollected;
        public void PressedFButton()
        {
            PressFButton?.Invoke();
        }

        public static void OnSpecialAttackEnd()
        {
            SpecialAttackEnd?.Invoke();
        }

        public static void OnCoinCollected()
        {
            CoinCollected?.Invoke();
        }
    }
}
