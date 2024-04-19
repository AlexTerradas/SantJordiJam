using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public RythmPointController m_RythmPointController;

    [Header("DANCE POINT")]
    public RectTransform m_Panel;
    public float m_PanelLeft;
    public float m_PanelRight;
    public RectTransform m_DancePoint;
    public float m_YMovementSpeed;
    public float m_XMovementSpeed;
    float m_MovementRange;
    float m_SongTimer;

    [Header("Timer")]
    public TextMeshProUGUI m_TimerText;

	private void Awake()
	{
        m_MovementRange=m_Panel.offsetMax.y;
        m_PanelLeft=m_Panel.offsetMin.x;
        m_PanelRight=m_Panel.offsetMax.x-m_Panel.offsetMin.x;
        m_SongTimer=0.0f;
	}
	void Update()
    {
        m_SongTimer+=Time.deltaTime;

        Vector3 l_DancePointPos=m_DancePoint.localPosition;
        l_DancePointPos.y=Mathf.Sin(m_SongTimer*m_YMovementSpeed)*m_MovementRange;
        if(Input.GetKey(KeyCode.LeftArrow))
            l_DancePointPos.x-=m_XMovementSpeed*Time.deltaTime;
        else if(Input.GetKey(KeyCode.RightArrow))
            l_DancePointPos.x+=m_XMovementSpeed*Time.deltaTime;
        l_DancePointPos.x=Mathf.Clamp(l_DancePointPos.x, 0.0f, m_PanelRight);
        m_DancePoint.localPosition=l_DancePointPos;
        
        m_TimerText.text=m_SongTimer.ToString();
        
        //Vector2 l_RythmPointPos=m_RythmPointController.GetCurrentRythmPoint().GetPosition();
		//Vector2 l_PlayerPointPos=m_DancePoint.localPosition;
		//Debug.Log("Point pos: "+l_RythmPointPos+" Player pos: "+l_PlayerPointPos+" DIstance: "+Vector2.Distance(l_RythmPointPos, l_PlayerPointPos));
        
        //float l_DistanceToPointY=Mathf.Abs(l_PlayerPointPos.y-l_RythmPointPos.y);
        //float l_TotalDistanceToPoint=Vector2.Distance(l_RythmPointPos, l_PlayerPointPos);
        
        //if(l_DistanceToPointY<=m_RythmPointController.m_MissRangeToInteract)
        //{
        //          m_RythmPointController.SetTimingCircleSize(l_DistanceToPointY);
        //}

        //if(l_TotalDistanceToPoint<=m_RythmPointController.m_PerfectRangeToInteract)
        //{
        //}
        //else if(l_TotalDistanceToPoint<=m_RythmPointController.m_GoodRangeToInteract)
        //{
        //}
        //else if(l_TotalDistanceToPoint<=m_RythmPointController.m_BadRangeToInteract)
        //{
        //}

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
		//	if(Vector2.Distance(l_RythmPointPos, l_PlayerPointPos)<=m_RythmPointController.m_MissRangeToInteract)
        //  {
        //    m_RythmPointController.GetCurrentRythmPoint().DisablePoint(true);
        //    m_RythmPointController.IncreaseCurrentRythmPoint();
        //  }
		//}

        float l_TimeToReachPoint=m_RythmPointController.GetCurrentRythmPoint().GetSongTime()-m_SongTimer;

        if(l_TimeToReachPoint<=m_RythmPointController.m_MissRangeToInteract)
        {
            m_RythmPointController.SetTimingCircleSize(l_TimeToReachPoint);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
			if(l_TimeToReachPoint<=m_RythmPointController.m_MissRangeToInteract)
            {
                m_RythmPointController.GetCurrentRythmPoint().DisablePoint(true);
                m_RythmPointController.IncreaseCurrentRythmPoint();
            }
		}

    }
    public float GetMinPosX()
    {
        return m_PanelLeft;
    }
    public float GetMaxPosX()
    { 
        return m_PanelRight;
    }
}