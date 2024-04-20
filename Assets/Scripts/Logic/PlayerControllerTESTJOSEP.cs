using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerTESTJOSEP : MonoBehaviour
{
    public RythmPointController m_RythmPointController;

    [Header("DANCE POINT")]
    public RectTransform m_Panel;
    float m_PanelLeft;
    float m_PanelRight;
    public RectTransform m_DancePoint;
    public float m_YMovementSpeed;
    public float m_XMovementSpeed;
    float m_MovementRange;

    [Header("Puntuation")]
    [SerializeField] private string[] pointsLevelText = {"FAIL", "OK", "GOOD", "PERFECT"};
    [SerializeField] private PuntuationPopup puntuationTextPopup;


	private void Start()
	{
        m_MovementRange=m_Panel.offsetMax.y;
        m_PanelLeft=m_Panel.offsetMin.x;
        m_PanelRight=m_Panel.offsetMax.x-m_Panel.offsetMin.x;
        //puntuationTextPopup.Constructor(transform, m_Panel, pointsLevelText[2]);
	}
	void Update()
    {
        Vector3 l_DancePointPos=m_DancePoint.localPosition;
        l_DancePointPos.y=Mathf.Sin(Time.time*m_YMovementSpeed)*m_MovementRange;
        if(Input.GetKey(KeyCode.LeftArrow))
            l_DancePointPos.x-=m_XMovementSpeed*Time.deltaTime;
        else if(Input.GetKey(KeyCode.RightArrow))
            l_DancePointPos.x+=m_XMovementSpeed*Time.deltaTime;
        l_DancePointPos.x=Mathf.Clamp(l_DancePointPos.x, 0.0f, m_PanelRight);
        m_DancePoint.localPosition=l_DancePointPos;
        
        Vector2 l_RythmPointPos=m_RythmPointController.GetCurrentRythmPoint().GetPosition();
		Vector2 l_PlayerPointPos=m_DancePoint.localPosition;
		//Debug.Log("Point pos: "+l_RythmPointPos+" Player pos: "+l_PlayerPointPos+" DIstance: "+Vector2.Distance(l_RythmPointPos, l_PlayerPointPos));
        
        float l_DistanceToPointY=Mathf.Abs(l_PlayerPointPos.y-l_RythmPointPos.y);
        float l_TotalDistanceToPoint=Vector2.Distance(l_RythmPointPos, l_PlayerPointPos);
        
        if(l_DistanceToPointY<=m_RythmPointController.m_MissRangeToInteract)
        {
            m_RythmPointController.SetTimingCircleSize(l_DistanceToPointY);
            puntuationTextPopup.Constructor(m_RythmPointController.GetCurrentRythmPoint().transform, m_Panel, pointsLevelText[0], false);
        }

        if(l_TotalDistanceToPoint<=m_RythmPointController.m_PerfectRangeToInteract)
        {
            puntuationTextPopup.Constructor(m_RythmPointController.GetCurrentRythmPoint().transform, m_Panel, pointsLevelText[3], true);
        }
        else if(l_TotalDistanceToPoint<=m_RythmPointController.m_GoodRangeToInteract)
        {
            puntuationTextPopup.Constructor(m_RythmPointController.GetCurrentRythmPoint().transform, m_Panel, pointsLevelText[2], false);
        }
        else if(l_TotalDistanceToPoint<=m_RythmPointController.m_BadRangeToInteract)
        {
            puntuationTextPopup.Constructor(m_RythmPointController.GetCurrentRythmPoint().transform, m_Panel, pointsLevelText[1], false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
			if(Vector2.Distance(l_RythmPointPos, l_PlayerPointPos)<=m_RythmPointController.m_MissRangeToInteract)
            {
                m_RythmPointController.GetCurrentRythmPoint().DisablePoint(true);
                m_RythmPointController.IncreaseCurrentRythmPoint();
            }
		}

        //print("FORWARD:" + m_DancePoint-gameObject.GetComponent<Transform>().eulerAngles);

        // if(Input.GetMouseButtonDown(0))
        // {
        //     puntuationTextPopup.Constructor(transform, m_Panel, pointsLevelText[0], false);
        // }
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