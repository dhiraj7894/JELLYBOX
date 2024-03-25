using System;

namespace Jelly.Core
{
    public class EventManager : Singleton<EventManager>
    {
        public event Action PressFButton;
        public static event Action SpecialAttackEnd;
        public static event Action OrbCollected;
        public static event Action CutSceneChange;
        public void PressedFButton()
        {
            PressFButton?.Invoke();
        }

        public static void OnSpecialAttackEnd()
        {
            SpecialAttackEnd?.Invoke();
        }

        public static void OnOrbCollected()
        {
            OrbCollected?.Invoke();
        }

        public static void OnCutSceneChange()
        {
            CutSceneChange?.Invoke();
        }
    }
}
