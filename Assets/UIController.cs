using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("SLIDER")]
    public RectTransform m_Panel;
    public RectTransform m_SliderObject;
    Slider m_Slider;
    public RectTransform m_DancePoint;
    public float m_YMovementSpeed;
    public float m_XMovementSpeed;
    float m_MovementRange;

	private void Start()
	{
		m_Slider=m_SliderObject.gameObject.GetComponent<Slider>();
        m_MovementRange=m_Panel.offsetMax.y;
        //Debug.Log(m_Panel.offsetMin.x);
        //Debug.Log(m_Panel.offsetMin.y);
        //Debug.Log(m_Panel.offsetMax.x);
        //Debug.Log(m_Panel.offsetMax.y);
        Debug.Log(Mathf.Sin(10.0f*m_YMovementSpeed)*m_MovementRange);
	}
	void Update()
    {
        if(Time.time<=10.0f)
        {
            Vector3 l_Position=m_SliderObject.localPosition;
            l_Position.y=Mathf.Sin(Time.time*m_YMovementSpeed)*m_MovementRange;
            m_SliderObject.localPosition=l_Position;

            if(Input.GetKey(KeyCode.LeftArrow))
                m_Slider.value-=m_XMovementSpeed*Time.deltaTime;
            else if(Input.GetKey(KeyCode.RightArrow))
                m_Slider.value+=m_XMovementSpeed*Time.deltaTime;
        }
    }
}