using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    private bool active;
    private int currentID;
    
    private void Start()
    {
        currentID = PlayerPrefs.GetInt("LanguageKey", 0);
        ChangeLanguage(currentID);
    }

    public void IncreaseID()
    {
        if (currentID >= LocalizationSettings.AvailableLocales.Locales.Count -1) currentID = 0;
        else currentID++;
        ChangeLanguage(currentID);
    }

    public void DecreaseID()
    {
        if (currentID == 0) currentID = LocalizationSettings.AvailableLocales.Locales.Count -1;
        else currentID--;
        ChangeLanguage(currentID);
    }

    public void ChangeLanguage(int id)
    {
        if (active) return;
        StartCoroutine(SetLanguage(id));
    }
    
    IEnumerator SetLanguage(int id)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        PlayerPrefs.SetInt("LanguageKey", id);
        active = false;
    }
}
