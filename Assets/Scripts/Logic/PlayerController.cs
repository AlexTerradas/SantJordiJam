using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public RythmPointController m_RythmPointController;

    [Header("DANCE POINT")]
    public RectTransform m_Panel;
    float m_PanelLeft;
    float m_PanelRight;
    public RectTransform m_DancePoint;
    public float m_YMovementSpeed;
    public float m_XMovementSpeed;
    float m_DirectionX;
    float m_MovementRange;
    float m_SongTimer;

    [Header("Timer")]
    public TextMeshProUGUI m_TimerText;

    [Header("Score")]
    public string[] pointsLevelKey;
    [SerializeField] 
    private PuntuationPopup puntuationTextPopup;
    public TextMeshProUGUI m_ScoreText;
    float m_CurrentScore;

	private void Awake()
	{
        m_MovementRange=m_Panel.offsetMax.y;
        m_PanelLeft=0.0f;
        m_PanelRight=m_Panel.offsetMax.x-m_Panel.offsetMin.x;
        m_SongTimer=0.0f;
	}
	void Update()
    {
        m_SongTimer+=Time.deltaTime;

        Vector3 l_DancePointPos=m_DancePoint.localPosition;
        l_DancePointPos.y=Mathf.Sin(m_SongTimer*m_YMovementSpeed)*m_MovementRange;
        if(m_DirectionX<0.0f)
            l_DancePointPos.x-=m_XMovementSpeed*Time.deltaTime;
        else if(m_DirectionX>0.0f)
            l_DancePointPos.x+=m_XMovementSpeed*Time.deltaTime;
        l_DancePointPos.x=Mathf.Clamp(l_DancePointPos.x, 0.0f, m_PanelRight);
        m_DancePoint.localPosition=l_DancePointPos;
        
        m_TimerText.text=m_SongTimer.ToString();
        
        //Vector2 l_RythmPointPos=m_RythmPointController.GetCurrentRythmPoint().GetPosition();
		//Vector2 l_PlayerPointPos=m_DancePoint.localPosition;
		//Debug.Log("Point pos: "+l_RythmPointPos+" Player pos: "+l_PlayerPointPos+" DIstance: "+Vector2.Distance(l_RythmPointPos, l_PlayerPointPos));
        
        //float l_DistanceToPointY=Mathf.Abs(l_PlayerPointPos.y-l_RythmPointPos.y);
        //float l_TotalDistanceToPoint=Vector2.Distance(l_RythmPointPos, l_PlayerPointPos);

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
		//	if(Vector2.Distance(l_RythmPointPos, l_PlayerPointPos)<=m_RythmPointController.m_MissRangeToInteract)
        //  {
        //    m_RythmPointController.GetCurrentRythmPoint().DisablePoint(true);
        //    m_RythmPointController.IncreaseCurrentRythmPoint();
        //  }
		//}
        m_RythmPointController.SetTimingCircles(m_SongTimer);
    }
    public void MovePointXAxis(float Direction)
    {
        m_DirectionX=Direction;
    }
    public void InteractWithPoint()
    {
        float l_TimeToReachPoint=m_RythmPointController.GetCurrentRythmPoint().GetSongTime()-m_SongTimer;
		Vector2 l_RythmPointPos=m_RythmPointController.GetCurrentRythmPoint().GetPosition();
		Vector2 l_PlayerPointPos=m_DancePoint.localPosition;
        float l_TotalDistanceToPoint=Vector2.Distance(l_RythmPointPos, l_PlayerPointPos);
		if(l_TotalDistanceToPoint<=m_RythmPointController.m_PerfectPointRange && l_TotalDistanceToPoint>=-m_RythmPointController.m_PerfectPointRange)
        {
            ShowPointScoreParticles(pointsLevelKey[3], true);
            AddScore(m_RythmPointController.m_PerfectPointScore);
            m_RythmPointController.GetCurrentRythmPoint().DisablePoint();
            m_RythmPointController.IncreaseCurrentRythmPoint();
        }
        else if(l_TotalDistanceToPoint<=m_RythmPointController.m_GoodPointRange)
        {
            ShowPointScoreParticles(pointsLevelKey[2], false);
            AddScore(m_RythmPointController.m_GoodPointScore);
            m_RythmPointController.GetCurrentRythmPoint().DisablePoint();
            m_RythmPointController.IncreaseCurrentRythmPoint();
        }
        else if(l_TotalDistanceToPoint<=m_RythmPointController.m_BadPointRange)
        {
            ShowPointScoreParticles(pointsLevelKey[1], false);
            AddScore(m_RythmPointController.m_BadPointScore);
            m_RythmPointController.GetCurrentRythmPoint().DisablePoint();
            m_RythmPointController.IncreaseCurrentRythmPoint();
        }
        else if(l_TotalDistanceToPoint<=m_RythmPointController.m_MissPointRange)
        {
            ShowPointScoreParticles(pointsLevelKey[0], false);
            m_RythmPointController.GetCurrentRythmPoint().DisablePoint();
            m_RythmPointController.IncreaseCurrentRythmPoint();
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
    public float GetMovementSpeedY()
    {
        return m_YMovementSpeed;
    }
    public float GetMovementRange()
    {
        return m_MovementRange;
    }
    public void ShowPointScoreParticles(string Text, bool IsPerfect)
    {
        puntuationTextPopup.Constructor(m_RythmPointController.GetCurrentRythmPoint().transform, m_Panel, Text, IsPerfect);
    }
    void AddScore(int Score)
    {
        m_CurrentScore+=Score;
        GameManager.instance.playerScore = m_CurrentScore;
        m_ScoreText.SetText(m_CurrentScore.ToString()+" - "+(m_CurrentScore/m_RythmPointController.GetMaxScore()*100.0f).ToString("#.00")+"%");
    }
}