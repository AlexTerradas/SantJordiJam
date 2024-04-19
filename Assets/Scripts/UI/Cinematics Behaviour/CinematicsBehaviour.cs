using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CinematicsBehaviour : MonoBehaviour
{
    [SerializeField] ChooseYourBitch bitch = ChooseYourBitch.NONE;
    [SerializeField] bool eventFired;

    [Header("Canvas images")]
    [SerializeField] List<Cinematic> cinematicImages;
    [SerializeField] Cinematic finalA;
    [SerializeField] Cinematic finalB;

    [Header("Cinematic Properties")]
    [SerializeField] Image image;
    [SerializeField] TextBubble textBubble;

    [Header("Cinematics Canvas")]
    [SerializeField] GameObject introCinematics;
    [SerializeField] GameObject choiceCinematics;

    void Awake()
    {
        if (cinematicImages != null)
        {
            StartCoroutine(ShowCinematic(cinematicImages[0], cinematicImages[1]));
        }
    }

    public IEnumerator ShowCinematic(Cinematic _cinematic, Cinematic _nextCinematic)
    {
        image.sprite = _cinematic.GetSprite();

        if(_cinematic.GetStringEvent() != "")
        {
            textBubble.OnDisplay(_cinematic.GetStringEvent());
            while(!textBubble.GetTextDisplayed())
                yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(_cinematic.GetTime());
            textBubble.DisableText();
        }
        else
        {
            if (_cinematic.GetHasInteraction())
            {
                choiceCinematics.gameObject.SetActive(true);

                while(!eventFired)
                    yield return new WaitForEndOfFrame();

                eventFired = false;
                choiceCinematics.gameObject.SetActive(false);
            }
            else
                yield return new WaitForSeconds(_cinematic.GetTime());
        }

        if(cinematicImages.Count-1 > cinematicImages.IndexOf(_nextCinematic))
            StartCoroutine(ShowCinematic(_nextCinematic, cinematicImages[cinematicImages.IndexOf(_nextCinematic)+1]));
        else
        {
            // canviar linea d'abaix per passar a gameplay <-- AQUI DARO RATA! hihi
            introCinematics.SetActive(false);
        }
    }

    /// <summary>
    /// 0 is Sant Jordi
    /// 1 is Princess
    /// </summary>
    /// <param name="_chooseYourBitch"></param>
    public void SelectYourChoice(int _chooseYourBitch)
    {
        bitch = (ChooseYourBitch)_chooseYourBitch;
        switch (_chooseYourBitch)
        {
            case 0:
                cinematicImages.Add(finalA);
                break;

            case 1:
                cinematicImages.Add(finalB);
                break;
        }
        eventFired = true;
    }
}
