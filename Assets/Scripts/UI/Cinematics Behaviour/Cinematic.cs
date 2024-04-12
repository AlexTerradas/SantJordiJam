using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cinematic", menuName = "JamSantJordi/Cinematic", order = 1)]
public class Cinematic : ScriptableObject
{
    [Header("Properties")]
    [SerializeField] Sprite sprite;
    [SerializeField] float time;

    [Header("Interaction")]
    [SerializeField] bool hasInteraction;

    public Sprite GetSprite() 
    { 
        return sprite; 
    }

    public float GetTime()
    {
        return time;
    }

    public bool GetHasInteraction()
    {
        return hasInteraction;
    }
}
