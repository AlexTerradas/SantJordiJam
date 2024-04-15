using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string[] _danceParameters;
    private Animator _animator;
    private float _danceAnimationDelay;
    private float _mistakeAnimationDelay;
    private int _dance;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(11);
        StartCoroutine(DanceAnimation());
    }

    public IEnumerator DanceAnimation()
    {
        int random = Random.Range(0, _danceParameters.Length);
        if (random == _dance)
            StartCoroutine(DanceAnimation());
        _dance = random;
        _animator.SetTrigger(_danceParameters[_dance]);
        
        yield return new WaitForSeconds(_danceAnimationDelay);
        StartCoroutine(DanceAnimation());
    }

    public IEnumerator MistakeAnimation()
    {
        _animator.SetTrigger("Dizzy");
        
        yield return new WaitForSeconds(_mistakeAnimationDelay);
        StartCoroutine(DanceAnimation());
    }

    public void FinalAnimation()
    {
        _animator.SetTrigger("FinalPose");
    }
}
