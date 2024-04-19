using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public EventReference MainMenu { get; private set; }
    [field: SerializeField] public EventReference Music { get; private set; }
    [field: SerializeField] public EventReference ButtonHover { get; private set; }
    [field: SerializeField] public EventReference ButtonClick { get; private set; }
    [field: SerializeField] public EventReference Mistake { get; private set; }
    [field: SerializeField] public EventReference Success { get; private set; }
    [field: SerializeField] public EventReference Combo { get; private set; }
    [field: SerializeField] public EventReference WinGame { get; private set; }
    [field: SerializeField] public EventReference LoseGame { get; private set; }
    
    public static AudioManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }
    
    public void PlaySong(EventInstance song)
    {
        PLAYBACK_STATE playbackState;
        song.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            song.start();
    }

    public void StopSong(EventInstance song)
    {
        song.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseSong(EventInstance song)
    {
        song.setPaused(true);
    }

    public void ResumeSong(EventInstance song)
    {
        song.setPaused(false);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
