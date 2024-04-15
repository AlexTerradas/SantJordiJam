using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    public enum GameState
    {
        Starting,
        Playing,
        Ending,
        Results,
    }

    public GameState gameState;
    
    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Game Manager in the scene");
        instance = this;
    }

    private void Start()
    {
        gameState = GameState.Starting;
        AudioManager.instance.PlayOneShot(AudioManager.instance.Music);
        StartCoroutine(StartPlayingState());
        StartCoroutine(StartEndingState());
        StartCoroutine(StartResultsState());
    }

    IEnumerator StartPlayingState()
    {
        yield return new WaitForSeconds(10);
        gameState = GameState.Playing;
        //DanceAnimation
    }

    IEnumerator StartEndingState()
    {
        yield return new WaitForSeconds(180);
        gameState = GameState.Ending;
        //FinalPose
        
    }

    IEnumerator StartResultsState()
    {
        yield return new WaitForSeconds(185f);
        gameState = GameState.Results;
        //WinLoseAnimation
    }
}
