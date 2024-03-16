using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Core;
using Ink.Runtime;

namespace Game.Dialouge
{

    public class HiddenBoss1 : QuestStep
    {
        public TextAsset inkJSON;

        private void Start()
        {
            DialogueManager.Instance.EnterDialogueMode(inkJSON, this);
        }
        

    }
}
