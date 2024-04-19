using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RythmPoint : MonoBehaviour
{
    RectTransform m_RectTransform;
    public TextMeshProUGUI m_KeyText;
    int m_OrderIndex;

    [Header("TIMING CIRCLE")]
    public RectTransform m_TimingCircle;
    public float m_MaxWidth;
    public float m_MinWidth;
    [Range(0.0f, 1.0f)]
    public float m_MinCircleOpacity;
    Image m_TimingCircleImage;
    float m_SongTime;

	private void Awake()
	{
        m_RectTransform=gameObject.GetComponent<RectTransform>();
        m_TimingCircleImage=m_TimingCircle.gameObject.GetComponent<Image>();
        m_TimingCircle.gameObject.SetActive(false);
	}
    public void SetSongTime(float Time)
    {
        m_SongTime=Time;
    }
    public float GetSongTime()
    {
        return m_SongTime;
    }
    public void ShowTimingCircle()
    {
        if(!m_TimingCircle.gameObject.activeSelf)
        {
            m_TimingCircle.gameObject.SetActive(true);
            m_TimingCircle.sizeDelta=new Vector2(m_MaxWidth, m_MaxWidth);
            Color l_CircleColor=m_TimingCircleImage.color;
            l_CircleColor.a=m_MinCircleOpacity;
            m_TimingCircleImage.color=l_CircleColor;
        }
    }
    public void SetTimingCircleSize(float Pct)
    {
        float l_CircleSize=Mathf.Lerp(m_MinWidth, m_MaxWidth, Pct);
        m_TimingCircle.sizeDelta=new Vector2(l_CircleSize, l_CircleSize);
        Color l_CircleColor=m_TimingCircleImage.color;
        l_CircleColor.a=Mathf.Lerp(m_MinCircleOpacity, 1.0f, 1.0f-Pct);
        m_TimingCircleImage.color=l_CircleColor;
    }
    public Vector2 GetPosition()
    {
        return m_RectTransform.localPosition;
    }
    public void SetPosition(float Time, float PosX)
    {
        Vector3 l_LocalPosition=m_RectTransform.localPosition;
        l_LocalPosition.y=Mathf.Sin(Time*1.0f)*360.0f;
        l_LocalPosition.x=PosX;
        m_RectTransform.localPosition=l_LocalPosition;
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
