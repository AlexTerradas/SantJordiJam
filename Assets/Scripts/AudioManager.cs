using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public EventReference MainMenu { get; private set; }
    [field: SerializeField] public EventReference ButtonHover { get; private set; }
    [field: SerializeField] public EventReference ButtonClick { get; private set; }
    [field: SerializeField] public EventReference Mistake { get; private set; }
    [field: SerializeField] public EventReference Success { get; private set; }
    [field: SerializeField] public EventReference Combo { get; private set; }
    [field: SerializeField] public EventReference WinGame { get; private set; }
    [field: SerializeField] public EventReference LoseGame { get; private set; }
    [field: SerializeField] public EventReference Music { get; private set; }
    public static AudioManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Audio Manager in the scene");
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void StopAudio()
    {
        RuntimeManager.PauseAllEvents(true);
    }
}
