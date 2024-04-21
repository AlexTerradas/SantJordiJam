using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CCompassList
{
    public int m_CompassAmount;
    public List<float> m_Bpms;
}

public class RythmPointController : MonoBehaviour
{
    [Header("POINT GENERATOR")]
    public float m_CompassDuration;
    public float m_StartDelay;
    public float m_LimitOffset;
    public int m_MaxRandomRangeX;
    public float m_MaxDistanceY;
    public int m_NextPointsOnScreen;
    public List<CCompassList> m_CompassList;
    List<RythmPoint> m_Points=new List<RythmPoint>();
    int m_CurrentRythmPointIndex;

    [Header("GAME PARAMETERS")]
    [Header("Point Interaction Range")]
    public float m_MinRangeToShowCircles;
    public float m_MaxRangeToShowCircles;
    public float m_MissPointRange; 
    public float m_BadPointRange; 
    public float m_GoodPointRange; 
    public float m_PerfectPointRange; 
    [Header("Point Scores")]
    [Range(0.0f, 1.0f)]
    public float m_ScoreNeededPct;
    float m_MaxScore;
    float m_ScoreNeeded;
    public int m_BadPointScore;
    public int m_GoodPointScore;
    public int m_PerfectPointScore;
    
    [Header("References")]
    public GameObject m_RythmPointPrefab;
    public PlayerController m_PlayerController;

	private void Start()
	{
        m_PlayerController.SetStartSong();
        AudioManager.instance.PlaySong(AudioManager.instance.inGameSong);
        float l_Time=m_StartDelay;
        float l_RandomPosX=m_PlayerController.GetMaxPosX()/2;
        float l_LastPosY=0.0f;
        int l_SpriteIndex=0;
        //float l_LastPosX=0.0f;
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

                    l_RythmPointScript.SetSprite(l_SpriteIndex);
                    if(l_SpriteIndex+1<l_RythmPointScript.m_PointSprites.Count)
                        l_SpriteIndex++;
                    else
                        l_SpriteIndex=0;

                    l_RythmPointScript.SetSongTime(l_Time);

                    float l_DistanceY=Mathf.Abs((Mathf.Sin(l_Time*m_PlayerController.GetMovementSpeedY())*m_PlayerController.GetMovementRange())-l_LastPosY);
                    float l_RandomRangeXPct=Mathf.InverseLerp(0.0f, m_MaxDistanceY, l_DistanceY); 
                    if(l_RandomRangeXPct<=0.0f)
                        l_RandomRangeXPct=0.1f;
                    l_RandomPosX=Random.Range(l_RandomPosX-m_MaxRandomRangeX*l_RandomRangeXPct, l_RandomPosX+m_MaxRandomRangeX*l_RandomRangeXPct);
                    if(l_RandomPosX<=m_PlayerController.GetMinPosX()+m_LimitOffset)
                        l_RandomPosX=Random.Range(m_PlayerController.GetMinPosX()+m_LimitOffset, m_PlayerController.GetMinPosX()+m_LimitOffset+m_MaxRandomRangeX*l_RandomRangeXPct);
                    else if(l_RandomPosX>=m_PlayerController.GetMaxPosX()-m_LimitOffset)
                        l_RandomPosX=Random.Range(m_PlayerController.GetMaxPosX()-m_LimitOffset-m_MaxRandomRangeX*l_RandomRangeXPct, m_PlayerController.GetMaxPosX()-m_LimitOffset);
                    //Debug.Log("Pct: "+l_RandomRangeXPct+"  CurrentY: "+(Mathf.Sin(l_Time*m_PlayerController.GetMovementSpeedY())*m_PlayerController.GetMovementRange())+
                    //    "  LastY:"+l_LastPosY+"   CurrentX: "+l_RandomPosX+"   LastX: "+l_LastPosX+"   Distance: "+l_DistanceY);
				    l_RythmPointScript.SetPosition(l_Time, m_PlayerController.GetMovementSpeedY(), m_PlayerController.GetMovementRange(), l_RandomPosX);
                    l_LastPosY=l_RythmPointScript.GetPosition().y;

                    //l_LastPosX=l_RythmPointScript.GetPosition().x;
			        l_RythmPoint.SetActive(false);
				    m_Points.Add(l_RythmPointScript);
			    }
                l_Time+=l_RemainingTime;
            }
		}
        m_CurrentRythmPointIndex=0;
		m_Points[0].gameObject.SetActive(true);
		for(int i=0; i<m_NextPointsOnScreen; ++i)
        {
		    m_Points[1+i].gameObject.SetActive(true);
            m_Points[1+i].ShowTimingCircle();
        }
        m_MaxScore=m_Points.Count*m_PerfectPointScore;
        m_ScoreNeeded=m_MaxScore*m_ScoreNeededPct;
        m_PlayerController.m_DancePoint.SetAsLastSibling();
        GameManager.instance.maxScore = m_MaxScore;
        GameManager.instance.scoreNeededToWin = m_ScoreNeeded;
    }
    public RythmPoint GetPointByIndex(int Index)
    {
        return m_Points[Index];
    }
	public RythmPoint GetCurrentRythmPoint()
    {
        return m_Points[m_CurrentRythmPointIndex];
    }
    public int GetCurrentRythmPointIndex()
    {
        return m_CurrentRythmPointIndex;
    }
    public void IncreaseCurrentRythmPoint()
    {
        if(m_CurrentRythmPointIndex+1<m_Points.Count)
        {
            m_CurrentRythmPointIndex++;
            if(m_CurrentRythmPointIndex+m_NextPointsOnScreen<m_Points.Count)
                ShowPoint(m_CurrentRythmPointIndex+m_NextPointsOnScreen);
        }
        else
        {
            Debug.Log("YIPPEEE FINISHED");
        }
    }
    void ShowPoint(int Point)
    {
        m_Points[Point].gameObject.SetActive(true);
        m_Points[Point].ShowTimingCircle();
    }
    public void SetTimingCircles(float SongTime)
    {
        for(int i=m_CurrentRythmPointIndex; i<=m_CurrentRythmPointIndex+m_NextPointsOnScreen; ++i)
        {
            if(i>=m_Points.Count)
                break;
            RythmPoint l_Point=m_Points[i];
            float l_TimeToReachPoint=GetPointByIndex(i).GetSongTime()-SongTime;
            if(l_TimeToReachPoint>m_MinRangeToShowCircles)
            {
                if(l_Point.m_TimingCircle.gameObject.activeSelf)
                    l_Point.m_TimingCircle.gameObject.SetActive(false);
                continue;
            }
            if(!l_Point.m_TimingCircle.gameObject.activeSelf)
                l_Point.m_TimingCircle.gameObject.SetActive(true);
            if(l_TimeToReachPoint<=-m_MaxRangeToShowCircles)
            {
                m_PlayerController.ShowPointScoreParticles(m_PlayerController.pointsLevelKey[0], false);
                l_Point.DisablePoint();
                IncreaseCurrentRythmPoint();   
            }
            float l_Pct=Mathf.InverseLerp(m_MaxRangeToShowCircles, m_MinRangeToShowCircles, l_TimeToReachPoint);
            l_Point.SetTimingCircleSize(l_Pct);
        }
    }
    public float GetScoreNeeded()
    {
        return m_ScoreNeeded;
    }
    public float GetMaxScore()
    {
        return m_MaxScore;
    }
}
