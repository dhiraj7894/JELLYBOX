using System.Collections;
using System.Collections.Generic;
using UnityEditor.VFX.UI;
using UnityEngine;
using TMPro;
using Ink.Runtime;

namespace Jelly.Core
{

    public class DialogueManager : Singleton<DialogueManager>
    {
        [Header("Dialogue UIs")]
        public GameObject dialogueUI;
        public List<GameObject> uIToDesableOnDialogue = new List<GameObject>();

        [Header("Dialouge Elements")]
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI displaySpeakerName;

        [Header("Dialouge Audio Elements")]
        public VoiceLineSO voiceLineSO;
        public AudioSource audioSource;

        [Space(5)]
        public GameObject continueImage;


        public float typingSpeed = 0.04f;


        public bool canContinueToNextLine = false;

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
            displaySpeakerName.text = "";
            isDialoguePlaying = true;
            SetActiveUIObjects(true);
            ContinueStory();
            
        }

        Coroutine displayLineCoroutine;
        void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                audioSource.Stop();                
                if (displayLineCoroutine != null)
                {
                    StopCoroutine(displayLineCoroutine);
                }
                displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
                HandleTags(currentStory.currentTags);
                //dialogueText.text = currentStory.Continue();

            }
            else
            {
                
                ExitDialogueMode();
            }
        }



        IEnumerator DisplayLine(string line)
        {
            //Before text line is shown in dialogue panal
            dialogueText.text = "";
            canContinueToNextLine = false;
            continueImage.SetActive(false);
            //PlayDialogueClip(dialogueSoundClip, true);
            ////

            int i = 0;
            foreach (char letter in line.ToCharArray())
            {
                if (InputActions._submit.triggered && i>= (line.Length/2))
                {
                    dialogueText.text = line.ToString();
                    break;
                }

                dialogueText.text += letter;
                i++;
                yield return new WaitForSeconds(typingSpeed);
            }


            ///
            //After text line is shown in dialogue panal
            //PlayDialogueClip(dialogueSoundClip, false);
            canContinueToNextLine = true;
            continueImage.SetActive(true);
        }


        void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                string[] splitTags = tag.Split(':');
                if (splitTags.Length != 2)
                {
                    Debug.LogError("Tag could not be correctly written: " + tag);
                }

                string tagKey = splitTags[0].Trim();
                string tagValue = splitTags[1].Trim();
                switch (tagKey)
                {
                    case INKTags.SPEAKER:
                        displaySpeakerName.text = tagValue;
                        break;
                    case INKTags.VOICELINE:
                        GetVoiceClipFromSO(tagValue);
                        break;
                    default:
                        Debug.Log("Nothing to show here");
                        break;
                }
            }

        }
        public void GetVoiceClipFromSO(string vcName)
        {

            foreach (AudioClip item in voiceLineSO.voiceClips)
            {
                if (item.name.Contains(vcName))
                {
                    audioSource.clip = item;
                    audioSource.Play();
                }
            }            
        }

        public void ExitDialogueMode()
        {
            audioSource.Stop();
            audioSource = null;
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

            if (InputActions._submit.triggered && canContinueToNextLine)
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
