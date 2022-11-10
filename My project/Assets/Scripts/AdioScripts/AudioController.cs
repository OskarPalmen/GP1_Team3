using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixerGroup audioMixerGroup;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }

  
}
