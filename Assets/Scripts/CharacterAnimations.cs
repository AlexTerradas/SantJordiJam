using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string[] _danceParameters;
    [SerializeField] private float _danceAnimationDelay;
    [SerializeField] private float _mistakeAnimationDelay;
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
        int random = Random.Range(0, _danceParameters.Length);
        if (random == _value)
            StartCoroutine(DanceAnimation());
        _value = random;
        _animator.SetTrigger(_danceParameters[_value]);
        
        yield return new WaitForSeconds(_danceAnimationDelay);
        
        if (GameManager.instance.gameState == GameManager.GameState.Playing && !_mistake)
            StartCoroutine(DanceAnimation());
    }

    public IEnumerator MistakeAnimation()
    {
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
        if (win)
        {
            _animator.SetTrigger("Win");
            AudioManager.instance.PlayOneShot(AudioManager.instance.WinGame);
        }
        else
        {
            _animator.SetTrigger("Lose");
            AudioManager.instance.PlayOneShot(AudioManager.instance.LoseGame);
        }
    }
}