using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class TextBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] float letterDelay = 0.1f;
    [SerializeField] bool textDisplayed = false;

    public void OnDisplay(string text)
    {
        StartCoroutine(ShowTextWithSound(text));
    }

    IEnumerator ShowTextWithSound(string text)
    {

        textDisplayed = false;
        string _text = text;
        textMeshPro.enabled = true;
        textMeshPro.text = "";

        for (int i = 0; i < _text.Length; i++)
        {
            textMeshPro.text += _text[i];
            yield return new WaitForSeconds(letterDelay);
        }
        textDisplayed = true;
        textMeshPro.enabled = false;
    }

    public bool GetTextDisplayed()
    {
        return textDisplayed;
    }
}
