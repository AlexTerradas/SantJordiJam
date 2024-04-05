using TMPro;
using UnityEngine;

public class HighlightText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    [SerializeField] private TMP_FontAsset unhighlightedFont;
    [SerializeField] private TMP_FontAsset highlightedFont;
    [SerializeField] private TMP_FontAsset selectedFont;

    [SerializeField]private bool selected = false;

    private void OnEnable()
    {
        SetSelectedState(selected);
    }

    public void SetHighlightState(bool state)
    {
        if(!selected)
            Text.font = state ? highlightedFont : unhighlightedFont;
    }

    public void SetSelectedState(bool state)
    {
        selected = state;
        Text.font = selected ? selectedFont : unhighlightedFont;
    }
}
