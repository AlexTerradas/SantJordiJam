using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string[] _danceParameters;
    [SerializeField] private float _danceAnimationDelay;
    [SerializeField] private float _mistakeAnimationDelay;
    private Animator _animator;
    private int _value;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        //StartCoroutine(StartGame());
    }

    /*
    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(DanceAnimation());
    }
    */

    public IEnumerator DanceAnimation()
    {
        int random = Random.Range(0, _danceParameters.Length);
        if (random == _value)
            StartCoroutine(DanceAnimation());
        _value = random;
        _animator.SetTrigger(_danceParameters[_value]);
        
        yield return new WaitForSeconds(_danceAnimationDelay);
        
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
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
