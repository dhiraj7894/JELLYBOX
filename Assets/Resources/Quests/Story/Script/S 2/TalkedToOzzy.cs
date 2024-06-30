using Jelly.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Dialouge
{
    public class TalkedToOzzy : QuestStep
    {
        public TextAsset inkJSON;
        private void Start()
        {
            LeanTween.delayedCall(1, () => { DialogueManager.Instance.EnterDialogueMode(inkJSON, this); });

        }
    }
}
