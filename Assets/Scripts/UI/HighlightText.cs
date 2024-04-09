using TMPro;
using UnityEngine;

public class HighlightText : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void PointerEnter()
    {
        _text.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
    }

    public void PointerExit()
    {
        _text.fontStyle = FontStyles.Normal | FontStyles.UpperCase;
    }
}
