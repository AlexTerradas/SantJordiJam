using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private float _introTimer;
    public float playerScore;
    public float maxScore;
    public float scoreNeededToWin;
    private bool _playerWinning;

    [SerializeField] private GameObject _canvasWin;
    [SerializeField] private GameObject _canvasLose;

    [SerializeField] private GameObject _princess;
    [SerializeField] private GameObject _santJordi;
    [SerializeField] private Transform enemyPoint;
    [SerializeField] private EnterExitScene enterExitScene;
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
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            enterExitScene.FadeOutAndChangeScene("EndCinematicScene");
    }

    IEnumerator StartPlayingState()
    {
        yield return new WaitForSeconds(_introTimer);
        gameState = GameState.Playing;
        onPlayingState();
    }

    IEnumerator StartEndingState()
    {
        yield return new WaitForSeconds(182.5f);
        gameState = GameState.Ending;
        onEndingState();
    }

    IEnumerator StartResultsState()
    {
        yield return new WaitForSeconds(186f);
        gameState = GameState.Results;
        if (playerScore >= scoreNeededToWin)
        {
            _playerWinning = true;
            Instantiate(_canvasWin);
        }
        else
        {
            _playerWinning = false;
            Instantiate(_canvasLose);
        }
        onResultsState(_playerWinning);
    }
}
