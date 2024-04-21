using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCinematicBehaviour : MonoBehaviour
{
    [SerializeField] Cinematic endCinematic;
    [SerializeField] Image image;
    
    void Awake()
    {
        AudioManager.instance.PlaySong(AudioManager.instance.cinematicSong);
        endCinematic = SantJordiJamLogic.GetLogic().GetEndCinematic();
        StartCoroutine(ShowEndCinematic());
    }

    IEnumerator ShowEndCinematic()
    {
        image.sprite = endCinematic.GetSprite();
        yield return new WaitForSeconds(endCinematic.GetTime());
        AudioManager.instance.StopSong(AudioManager.instance.cinematicSong);
        GetComponent<EnterExitScene>().FadeOutAndChangeScene("MainMenu");
    }
}
