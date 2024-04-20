using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    [SerializeField] private Color[] colorsArray = new Color[3];
    [SerializeField] private Light currentLight;
    [SerializeField] private float lightDuration = 0.1f;

    private int _value;
    IEnumerator FlashColor(float duration)
    {
        currentLight.color = colorsArray[GetRandomNumber()];

        yield return new WaitForSeconds(duration);

        //if (true)
        if (GameManager.instance.gameState == GameManager.GameState.Playing && GameManager.instance.gameState == GameManager.GameState.Starting)
        {
            StartCoroutine(FlashColor(lightDuration));
        }
    }
    void Start()
    {
        StartCoroutine(FlashColor(lightDuration));
    }
    public int GetRandomNumber()
    {
        int rnd = Random.Range(0, colorsArray.Length);
        if(rnd == _value)
            GetRandomNumber();
        _value = rnd;
        return _value;
    }
}
