using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string[] _danceParameters;
    [SerializeField] private float _danceAnimationDelay;
    [SerializeField] private float _mistakeAnimationDelay;
    [SerializeField] private bool _isNPC;
    private Animator _animator;
    private bool _mistake;
    private int _value;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.onPlayingState += StartDancing;
        GameManager.onEndingState += FinalAnimation;
        GameManager.onResultsState += WinLoseAnimation;
    }

    private void OnDisable()
    {
        GameManager.onPlayingState -= StartDancing;
        GameManager.onEndingState -= FinalAnimation;
        GameManager.onResultsState -= WinLoseAnimation;
    }

    public void StartDancing()
    {
        Debug.Log("La puta madre los eventos");
        StartCoroutine(DanceAnimation());
    }
    
    public IEnumerator DanceAnimation()
    {
        _animator.SetTrigger(_danceParameters[GetRandomNumber()]);
        
        yield return new WaitForSeconds(_danceAnimationDelay);
        
        if (GameManager.instance.gameState == GameManager.GameState.Playing && !_mistake)
            StartCoroutine(DanceAnimation());
    }

    public IEnumerator MistakeAnimation()
    {
        if (_mistake || GameManager.instance.gameState == GameManager.GameState.Starting)
            yield break;
        
        _mistake = true;
        _animator.SetTrigger("Dizzy");
        yield return new WaitForSeconds(_mistakeAnimationDelay);
        _mistake = false;
        
        if (GameManager.instance.gameState == GameManager.GameState.Playing) 
            StartCoroutine(DanceAnimation());
    }

    public void FinalAnimation()
    {
        _animator.SetTrigger("FinalPose");
    }
    
    public void WinLoseAnimation(bool win)
    {
        if (_isNPC)
            win = !win;
        
        if (win)
        {
            _animator.SetTrigger("Win");
            if (!_isNPC) AudioManager.instance.PlayOneShot(AudioManager.instance.WinGame);
        }
        else
        {
            _animator.SetTrigger("Defeat");
            if (!_isNPC) AudioManager.instance.PlayOneShot(AudioManager.instance.LoseGame);
        }
    }

    public int GetRandomNumber()
    {
        int random = Random.Range(0, _danceParameters.Length);
        if (random == _value)
            GetRandomNumber();
        _value = random;
        return _value;
    }
}