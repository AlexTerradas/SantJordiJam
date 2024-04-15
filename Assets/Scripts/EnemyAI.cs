using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private CharacterAnimations _animationController;
    [SerializeField] private float _mistakeTimerMin;
    [SerializeField] private float _mistakeTimerMax;
    private float _mistakeTimer;

    void Start()
    {
        _animationController = GetComponent<CharacterAnimations>();
        _mistakeTimer = Random.Range(_mistakeTimerMin, _mistakeTimerMax);
        StartCoroutine(TriggerMistake());
    }

    IEnumerator TriggerMistake()
    {
        yield return new WaitForSeconds(_mistakeTimer);
        
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            _animationController.StartCoroutine(_animationController.MistakeAnimation());
            _mistakeTimer = Random.Range(_mistakeTimerMin, _mistakeTimerMax);
            StartCoroutine(TriggerMistake());
        }
    }
}
