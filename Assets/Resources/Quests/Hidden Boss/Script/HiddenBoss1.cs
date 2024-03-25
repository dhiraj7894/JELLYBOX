using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Jelly.Core;
using Ink.Runtime;

namespace Jelly.Dialouge
{

    public class HiddenBoss1 : QuestStep
    {
        public TextAsset inkJSON;

        private void Start()
        {
            LeanTween.delayedCall(1, () =>{ DialogueManager.Instance.EnterDialogueMode(inkJSON, this); });
            
        }
        

    }
}
