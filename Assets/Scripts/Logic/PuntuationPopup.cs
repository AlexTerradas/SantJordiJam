using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;

public class PuntuationPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private TextMeshProUGUI text;
    RectTransform rectTransform;
    [SerializeField] private RectTransform damagePopupPosition;


    [Header("Values")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color perfectColor;
    private Color textCurrentColor;
    [SerializeField] private float speedX = 20.0f;
    [SerializeField] private float speedY = 20.0f;
    [SerializeField] private float duration = 2.0f;
    [SerializeField] private float disappearSpeed = 3.0f;
    [SerializeField] private int normalFontSize = 14;
    [SerializeField] private int perfectFontSize = 30;
    private LocalizeStringEvent stringEvent;
    
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        stringEvent = GetComponentInChildren<LocalizeStringEvent>();
        //text = transform.GetComponent<TextMeshPro>();
    }

    public PuntuationPopup Constructor(Transform currentTransform, RectTransform currentPanel, string currentText, bool isPerfect)
    {
        GameObject puntuationPopup = Instantiate(prefab, currentTransform.position, currentTransform.rotation, currentPanel);
        //text = puntuationPopup.GetComponent<TextMeshProUGUI>();
        PuntuationPopup puntuationTextPopup = puntuationPopup.GetComponent<PuntuationPopup>();
        puntuationTextPopup.Setup(currentText, isPerfect);
        return puntuationTextPopup;
    }

    public void Setup(string puntuationLevel, bool isPerfect)
    {
        stringEvent.SetEntry(puntuationLevel);
        //text.SetText(puntuationLevel);
        
        if (isPerfect)
        {
            text.fontSize = perfectFontSize;
            textCurrentColor = perfectColor;
        }
        else
        {
            text.fontSize = normalFontSize;
            textCurrentColor = normalColor;
        }
        
        //textCurrentColor = text.color;
        text.color = textCurrentColor;
    }

    private void Update()
    {
        transform.position += new Vector3(speedX, speedY) * Time.deltaTime;
        duration -= Time.deltaTime;

        if (duration < 0)
        {
            textCurrentColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textCurrentColor;

            if(textCurrentColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
