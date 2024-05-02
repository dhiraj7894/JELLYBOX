using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Jelly/AudioSO")]
public class VoiceLineSO : ScriptableObject
{
    public List<AudioClip> voiceClips = new List<AudioClip>();
}
