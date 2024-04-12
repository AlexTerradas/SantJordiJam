using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RythmPoint : MonoBehaviour
{
    public enum TKeyType
    {
        SPACE,
        OTHER
    }

    RectTransform m_RectTransform;
    public TextMeshProUGUI m_KeyText;
    int m_OrderIndex;
    bool m_EnteredRange;

    [Header("TIMING CIRCLE")]
    public RectTransform m_TimingCircle;
    public float m_MaxWidth;
    public float m_MinWidth;

	private void Awake()
	{
        m_RectTransform=gameObject.GetComponent<RectTransform>();
        m_TimingCircle.gameObject.SetActive(false);
	}
	public void SetKeyText(TKeyType Key)
    {
        if(Key==TKeyType.SPACE)
            m_KeyText.text="SPACE";
        else
            m_KeyText.text="G";
    }
    public void SetTimingCircleSize(float Pct)
    {
        if(!m_TimingCircle.gameObject.activeSelf)
        {
            m_TimingCircle.gameObject.SetActive(true);
        }

        float l_CircleSize=Mathf.Lerp(m_MinWidth, m_MaxWidth, Pct);
        m_TimingCircle.sizeDelta=new Vector2(l_CircleSize, l_CircleSize);
    }
    public Vector2 GetPosition()
    {
        return m_RectTransform.localPosition;
    }
    public void SetPosition(Vector2 Position)
    {
       m_RectTransform.localPosition=Position;
    }
    public void SetEnteredRange()
    {
        m_EnteredRange=true;
    }
    public bool GetEnteredRange()
    {
        return m_EnteredRange;
    }
    public void DisablePoint(bool Missed)
    {
        gameObject.SetActive(false);
    }

    public int GetIndex()
    {
        return m_OrderIndex;
    }
    public void SetIndex(int Index)
    {
        m_OrderIndex=Index;
    }
}
