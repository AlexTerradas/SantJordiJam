using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CCompassList
{
    public int m_CompassAmount;
    public List<float> m_Bpms;
    List<RythmPoint> m_Points=new List<RythmPoint>();

    public RythmPoint GetPoint(int Index) {return m_Points[Index];}
    public void SetPoint(RythmPoint Point) {m_Points.Add(Point);}
    public int GetTotalPoints() {return m_Points.Count;}
}

public class RythmPointController : MonoBehaviour
{
    [Header("POINT GENERATOR")]
    public float m_CompassDuration;
    public float m_LimitOffset;
    public int m_MaxRandomRangeX;
    public List<CCompassList> m_CompassList;

    [Header("Game Parameters")]
    public float m_MissRangeToInteract; 
    public float m_BadRangeToInteract; 
    public float m_GoodRangeToInteract; 
    public float m_PerfectRangeToInteract; 
    int m_CurrentSongPart;
    int m_CurrentRythmPoint;

    [Header("References")]
    public GameObject m_RythmPointPrefab;
    public PlayerController m_PlayerController;
    //public List<GameObject> m_RythmPointsPool;

	private void Start()
	{
        float l_Time=0.0f;
        float l_RandomPosX=Random.Range(m_PlayerController.m_PanelLeft+m_LimitOffset, m_PlayerController.m_PanelRight-m_LimitOffset);
		for(int i=0; i<m_CompassList.Count; ++i)
        {
            float l_PointsToSpawn=m_CompassList[i].m_Bpms.Count;
            for(int k=0; k<m_CompassList[i].m_CompassAmount; ++k)
            {
                int l_BpmsIndex=0;
                float l_RemainingTime=m_CompassDuration;
                for(int j=0; j<l_PointsToSpawn; ++j)
                {
                    l_Time+=m_CompassList[i].m_Bpms[l_BpmsIndex];
                    l_RemainingTime-=m_CompassList[i].m_Bpms[l_BpmsIndex];
                    l_BpmsIndex++;
				    GameObject l_RythmPoint=Instantiate(m_RythmPointPrefab, transform.position, transform.rotation, m_PlayerController.m_Panel);
				    RythmPoint l_RythmPointScript=l_RythmPoint.GetComponent<RythmPoint>();
                    l_RythmPointScript.SetSongTime(l_Time);
				    l_RythmPointScript.SetPosition(l_Time, l_RandomPosX);
                    l_RandomPosX=Random.Range(l_RandomPosX-m_MaxRandomRangeX, l_RandomPosX+m_MaxRandomRangeX);
                    if(l_RandomPosX<=m_PlayerController.m_PanelLeft+m_LimitOffset)
                        l_RandomPosX=Random.Range(m_PlayerController.m_PanelLeft+m_LimitOffset, m_PlayerController.m_PanelLeft+m_LimitOffset+m_MaxRandomRangeX);
                    else if(l_RandomPosX>=m_PlayerController.m_PanelRight-m_LimitOffset)
                        l_RandomPosX=Random.Range(m_PlayerController.m_PanelRight-m_LimitOffset-m_MaxRandomRangeX, m_PlayerController.m_PanelRight-m_LimitOffset);
			        l_RythmPoint.SetActive(false);
				    m_CompassList[i].SetPoint(l_RythmPointScript);
			    }
                l_Time+=l_RemainingTime;
            }
			//l_RythmPointScript.SetIndex(i);
		}
        m_CurrentSongPart=0;
        m_CurrentRythmPoint=0;
		m_CompassList[m_CurrentSongPart].GetPoint(0).gameObject.SetActive(true);
		m_CompassList[m_CurrentSongPart].GetPoint(1).gameObject.SetActive(true);
	}
	public RythmPoint GetCurrentRythmPoint()
    {
        return m_CompassList[m_CurrentSongPart].GetPoint(m_CurrentRythmPoint);
    }
    public void IncreaseCurrentRythmPoint()
    {
        if(m_CurrentRythmPoint+1<m_CompassList[m_CurrentSongPart].GetTotalPoints())
        {
            m_CurrentRythmPoint++;
            if(m_CurrentRythmPoint+1<m_CompassList[m_CurrentSongPart].GetTotalPoints())
                ShowPoint(m_CurrentSongPart, m_CurrentRythmPoint+1);
            else if(m_CurrentSongPart+1<m_CompassList.Count)
                ShowPoint(m_CurrentSongPart+1, 0);
        }
        else if(m_CurrentSongPart+1<m_CompassList.Count)
        {
            m_CurrentRythmPoint=0;
            m_CurrentSongPart++;
            ShowPoint(m_CurrentSongPart, m_CurrentRythmPoint+1);
        }
        else
        {
            Debug.Log("YIPPEEE FINISHED");
            Debug.Break();
        }
    }
    void ShowPoint(int SongPart, int Point)
    {
        m_CompassList[SongPart].GetPoint(Point).gameObject.SetActive(true);
        m_CompassList[SongPart].GetPoint(Point).ShowTimingCircle();
    }
    public void SetTimingCircleSize(float Distance)
    {
        RythmPoint l_Point=m_CompassList[m_CurrentSongPart].GetPoint(m_CurrentRythmPoint);
        if(Distance<=-m_PerfectRangeToInteract)
        {
            l_Point.DisablePoint(true);
            IncreaseCurrentRythmPoint();   
        }
        float l_Pct=Mathf.InverseLerp(m_PerfectRangeToInteract, m_MissRangeToInteract, Distance);
        l_Point.SetTimingCircleSize(l_Pct);
    }
}
