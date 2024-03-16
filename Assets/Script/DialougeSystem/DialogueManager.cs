using System.Collections;
using System.Collections.Generic;
using UnityEditor.VFX.UI;
using UnityEngine;
using TMPro;
using Ink.Runtime;

namespace Game.Core
{

    public class DialogueManager : Singleton<DialogueManager>
    {
        [Header("Dialogue UIs")]
        public GameObject dialogueUI;
        public List<GameObject> uIToDesableOnDialogue = new List<GameObject>();

        [Header("Dialouge Elements")]
        public TextMeshProUGUI dialogueText;
        public Story currentStory;
        public bool isDialoguePlaying { get; private set; }

        [Header("Quest Objects")]
        public QuestStep step;
        private void Start()
        {
            isDialoguePlaying = false;
            SetActiveUIObjects();
        }

        public void EnterDialogueMode(TextAsset inkJSON, QuestStep step = null)
        {
            currentStory = new Story(inkJSON.text);
            if (step != null)
            {
                this.step = step;
            }
            isDialoguePlaying = true;
            SetActiveUIObjects(true);
            ContinueStory();
            
        }
        void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                dialogueText.text = currentStory.Continue();
            }
            else
            {
                
                ExitDialogueMode();
            }
        }

        public void ExitDialogueMode()
        {
            isDialoguePlaying = false;
            dialogueText.text = "";
            SetActiveUIObjects();
            if (step != null)
            {
                step.FinishedQuestStep();
            }
        }


        private void Update()
        {
            if (!isDialoguePlaying)
            {
                return;
            }

            if (InputActions._submit.triggered || InputActions._jumpAction.triggered || InputActions._attack.triggered)
            {
                ContinueStory();
            }

        }




        /// <summary>
        /// This method handles Canavas object which need to active when we start dialogues
        /// List of objects will set active to false and the dialogue object will active true
        /// </summary>
        /// <param name="isTrue"></param>
        public void SetActiveUIObjects(bool isTrue = false)
        {
            dialogueUI.SetActive(isTrue);
            if (uIToDesableOnDialogue.Count > 0)
            {
                foreach (GameObject item in uIToDesableOnDialogue)
                {
                    item.SetActive(!isTrue);
                }
            }
        }































    }

    
}
