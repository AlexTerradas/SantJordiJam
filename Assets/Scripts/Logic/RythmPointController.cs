using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CRythmPointList
{
    public Vector2 m_Position;
    public RythmPoint.TKeyType m_Key;
    RythmPoint m_Point;

    public RythmPoint GetPoint() {return m_Point;}
    public void SetPoint(RythmPoint Point) {m_Point=Point;}
}

public class RythmPointController : MonoBehaviour
{
    [Header("POINT GENERATOR")]
    public List<CRythmPointList> m_RythmPointList;

    [Header("Game Parameters")]
    public float m_MissRangeToInteract; 
    public float m_BadRangeToInteract; 
    public float m_GoodRangeToInteract; 
    public float m_PerfectRangeToInteract; 
    int m_CurrentRythmPoint;

    [Header("References")]
    public GameObject m_RythmPointPrefab;
    public PlayerController m_PlayerController;
    //public List<GameObject> m_RythmPointsPool;

	private void Start()
	{
		for(int i=0; i<m_RythmPointList.Count; ++i)
        {
            GameObject l_RythmPoint=Instantiate(m_RythmPointPrefab, transform.position, transform.rotation, m_PlayerController.m_Panel);
            RythmPoint l_RythmPointScript=l_RythmPoint.GetComponent<RythmPoint>();
            l_RythmPointScript.SetIndex(i);
            l_RythmPointScript.SetPosition(m_RythmPointList[i].m_Position);
            l_RythmPointScript.SetKeyText(m_RythmPointList[i].m_Key);
            m_RythmPointList[i].SetPoint(l_RythmPointScript);
            l_RythmPoint.SetActive(false);
        }
        m_RythmPointList[0].GetPoint().gameObject.SetActive(true);
        m_RythmPointList[1].GetPoint().gameObject.SetActive(true);
	}
	public RythmPoint GetCurrentRythmPoint()
    {
        return m_RythmPointList[m_CurrentRythmPoint].GetPoint();
    }
    public void IncreaseCurrentRythmPoint()
    {
        if(m_CurrentRythmPoint+1<m_RythmPointList.Count)
        {
            m_CurrentRythmPoint++;
            if(m_CurrentRythmPoint+1<m_RythmPointList.Count)
                m_RythmPointList[m_CurrentRythmPoint+1].GetPoint().gameObject.SetActive(true);
        }
        else
            Debug.Log("FINISHED");
    }
    public void SetTimingCircleSize(float Distance)
    {
        RythmPoint l_Point=m_RythmPointList[m_CurrentRythmPoint].GetPoint();
        float l_Pct=Mathf.InverseLerp(m_PerfectRangeToInteract, m_MissRangeToInteract, Distance);
        l_Point.SetTimingCircleSize(l_Pct);
        if(!l_Point.GetEnteredRange() && Distance<=m_PerfectRangeToInteract)
        {
            l_Point.SetEnteredRange();
        }
        else if(l_Point.GetEnteredRange() && Distance>m_PerfectRangeToInteract)
        {
            l_Point.DisablePoint(true);
            IncreaseCurrentRythmPoint();   
        }
    }
}
