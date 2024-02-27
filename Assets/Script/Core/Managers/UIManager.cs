using Game.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        [Space(10)]
        public CanvasGroup BackScreenCutOut;

        [Space(5)]
        public TextMeshProUGUI CoinCount;


        public void CutSceneFadeOutIn(float cooldown)
        {
            LeanTween.value(this.gameObject, 0, 1, 0.4f).
                setOnUpdate((float val) => { BackScreenCutOut.alpha = val; }).
                setOnComplete(()=> { 
                    LeanTween.delayedCall(cooldown, () => { 
                        LeanTween.value(this.gameObject, 1, 0, 0.4f).setOnUpdate((float val) => { 
                            BackScreenCutOut.alpha = val; 
                        }); 
                    }); 
                });
        }

    }
}
