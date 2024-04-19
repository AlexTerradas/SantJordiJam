using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public class TextBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] float letterDelay = 0.1f;
    [SerializeField] bool textDisplayed = false; 
    string _text;

    public void OnDisplay(string text)
    {
        textMeshPro.enabled = false;
        LocalizedString _localizedStringe= GetComponent<LocalizeStringEvent>().StringReference;

        _localizedStringe.TableReference = "InGameTable";
        _localizedStringe.TableEntryReference = text;

        StartCoroutine(WaitForText());
    }

    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(0.15f);
        _text = textMeshPro.text;
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(ShowTextWithSound());
    }

    IEnumerator ShowTextWithSound()
    {
        textMeshPro.text = "";
        textDisplayed = false;
        textMeshPro.enabled = true;

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
