using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantJordiJamLogic : MonoBehaviour
{
    static SantJordiJamLogic instance;

    [Header("Bitch")]
    [SerializeField] ChooseYourBitch bitch;

    [Header("Cinematic")]
    [SerializeField] Cinematic finalA;
    [SerializeField] Cinematic finalB;

    [SerializeField] Cinematic cinematicEnd;

    [SerializeField] string keyPrincessRival;
    [SerializeField] string keySantJordiRival;
    [SerializeField] Cinematic cinematic;

    void Awake()
    {
        if(instance != null)
            Destroy(this);
        else
            instance = this;

        DontDestroyOnLoad(this);
        bitch = ChooseYourBitch.NONE;
        cinematicEnd = null;
    }

    public static SantJordiJamLogic GetLogic()
    {
        return instance;
    }

    public void SelectYourChoice(ChooseYourBitch _chooseYourBitch)
    {
        bitch = _chooseYourBitch; 
        switch (_chooseYourBitch)
        {
            case ChooseYourBitch.Princesa:
                cinematicEnd = finalA;
                cinematic.SetStringEvent(keySantJordiRival);
                break;

            case ChooseYourBitch.SantJordi:
                cinematicEnd = finalB;
                cinematic.SetStringEvent(keyPrincessRival);
                break;
        }
    }

    public ChooseYourBitch GetChooseYourBitch()
    {
        return bitch;
    }

    public Cinematic GetEndCinematic()
    {
        return cinematicEnd;
    }
}
