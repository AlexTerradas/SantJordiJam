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

    [Header("POINT SPRITE")]
    public Image m_PointImage;
    public List<Sprite> m_Sprites;

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
    public void SetPosition(float Time, float MovSpeed, float Range, float PosX)
    {
        Vector3 l_LocalPosition=m_RectTransform.localPosition;
        l_LocalPosition.y=Mathf.Sin(Time*MovSpeed)*Range;
        l_LocalPosition.x=PosX;
        m_RectTransform.localPosition=l_LocalPosition;
    }
    public Vector2 GetPosition()
    {
        return m_RectTransform.localPosition;
    }
    public void SetSprite(int Index)
    {
        m_PointImage.sprite=m_Sprites[Index];
    }

    public void DisablePoint()
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
