using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoader : MonoBehaviour
{
    public static AudioClip LoadResourceAudioClip(string path)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);

        if (audioClip == null)
        {
            Debug.LogWarning($"Audio clip at path '{path}' not found.");
            return null;
        }
        return audioClip;
    }
}
