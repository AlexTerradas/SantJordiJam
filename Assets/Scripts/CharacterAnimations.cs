using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private string[] _danceParameters;
    private Animator _animator;
    private int _danceValue;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(11);
        InvokeRepeating("DanceAnimation", 0.0f, 3.0f);
    }

    public void DanceAnimation()
    {
        int random = Random.Range(0, _danceParameters.Length);
        if (random == _danceValue)
            DanceAnimation();
        _danceValue = random;
        _animator.SetTrigger(_danceParameters[random]);
    }

    public void MistakeAnimation()
    {
        _animator.SetTrigger("Dizzy");
    }

    public void FinalAnimation()
    {
        _animator.SetTrigger("FinalPose");
    }
}
