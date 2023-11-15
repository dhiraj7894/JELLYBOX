using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{

    public class UIManager : Singleton<UIManager>
    {

        public Slider Health;
        public Slider Stamina;
        public Slider Shield;

        [Space(5)]
        public Slider SP_A;
        public Slider SP_B;
    }
}
