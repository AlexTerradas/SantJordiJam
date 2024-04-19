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
    [SerializeField] bool eventFired;

    [Header("Canvas images")]
    [SerializeField] List<Cinematic> cinematicImages;

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

        if(_cinematic.GetHasStringEvent())
        {
            textBubble.OnDisplay(_cinematic.GetStringEvent());
            while(!textBubble.GetTextDisplayed())
                yield return new WaitForSeconds(0.1f);

            yield return new WaitForSeconds(_cinematic.GetTime());
            textBubble.DisableText();
        }
        else
        {
            if (_cinematic.GetHasInteraction())
            {
                choiceCinematics.gameObject.SetActive(true);

                while(!eventFired)
                    yield return new WaitForSeconds(0.1f);

                eventFired = false;
                choiceCinematics.gameObject.SetActive(false);
            }
            else
                yield return new WaitForSeconds(_cinematic.GetTime());
        }

        if(cinematicImages.Count - 1 > cinematicImages.IndexOf(_nextCinematic))
            StartCoroutine(ShowCinematic(_nextCinematic, cinematicImages[cinematicImages.IndexOf(_nextCinematic)+1]));
        else
            GetComponent<EnterExitScene>().FadeOutAndChangeScene("CosasDaro");
    }

    /// <summary>
    /// 0 is Sant Jordi
    /// 1 is Princess
    /// </summary>
    /// <param name="_chooseYourBitch"></param>
    public void SelectYourChoice(int _chooseYourBitch)
    {
        SantJordiJamLogic.GetLogic().SelectYourChoice((ChooseYourBitch)_chooseYourBitch);
        eventFired = true;
    }
}
