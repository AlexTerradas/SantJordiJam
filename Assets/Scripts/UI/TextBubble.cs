using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        GetComponent<LocalizeStringEvent>().StringReference.SetReference("InGameTable", text); // PETA AQUESTA MERDA
        StartCoroutine(ShowTextWithSound());
    }

    IEnumerator ShowTextWithSound()
    {
        textDisplayed = false;
        
        string _text = textMeshPro.text;
        textMeshPro.enabled = true;

        yield return new WaitForSeconds(0.25f);
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
