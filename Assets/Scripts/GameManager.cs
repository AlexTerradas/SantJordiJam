using System.Collections;
using FMOD.Studio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private float _introTimer;
    private bool _playerWinning;

    [SerializeField] private GameObject _princess;
    [SerializeField] private GameObject _santJordi;
    [SerializeField] private Transform enemyPoint;
    
    public delegate void PlayingState();
    public static event PlayingState onPlayingState;
    
    public delegate void EndingState();
    public static event EndingState onEndingState;
    
    public delegate void ResultsState(bool win);
    public static event ResultsState onResultsState;
    
    public enum GameState
    {
        Starting,
        Playing,
        Ending,
        Results,
    }

    public GameState gameState;

    public ChooseYourBitch bitch;
    
    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Game Manager in the scene");
        instance = this;
    }

    private void Start()
    {
        gameState = GameState.Starting;
        StartCoroutine(StartPlayingState());
        StartCoroutine(StartEndingState());
        StartCoroutine(StartResultsState());

        if (SantJordiJamLogic.GetLogic().GetChooseYourBitch() == ChooseYourBitch.Princesa) 
            Instantiate(_santJordi, enemyPoint.position, enemyPoint.rotation);
        else 
            Instantiate(_princess, enemyPoint.position, enemyPoint.rotation);
        
        //EventInstance inGameSong = AudioManager.instance.CreateEventInstance(AudioManager.instance.Music);
        //AudioManager.instance.PlaySong(inGameSong);
    }

    IEnumerator StartPlayingState()
    {
        yield return new WaitForSeconds(_introTimer);
        gameState = GameState.Playing;
        onPlayingState();
    }

    IEnumerator StartEndingState()
    {
        yield return new WaitForSeconds(180);
        gameState = GameState.Ending;
        onEndingState();
    }

    IEnumerator StartResultsState()
    {
        yield return new WaitForSeconds(185f);
        gameState = GameState.Results;
        onResultsState(_playerWinning);
    }
}
