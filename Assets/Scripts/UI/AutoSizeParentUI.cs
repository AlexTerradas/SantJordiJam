using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class AutoSizeParentUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    
    void OnRectTransformDimensionsChange()
    {
        RectTransform rectt = GetComponent<RectTransform>();
        ((RectTransform)rectt.parent).sizeDelta = rectt.sizeDelta;
        
        if(rectTransform == null)
            return;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void SetParent(RectTransform rectTransform)
    {
        this.rectTransform = rectTransform;
    }
}
