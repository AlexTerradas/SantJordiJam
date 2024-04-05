using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TwitterShare : MonoBehaviour
{
    private readonly string twitterNameParameter = "Juega a este increíble juego creado por @andrew_raaya @JordiAlbaDev @Sergisggs @GuillemLlovDev @Belmontes_ART y @AinoaTAdev hecho para la #levelup_gamejam (@game_levelup)!\n\nAquí tenéis el link:\n\n";
    private readonly string twitterDescriptionParam = "";
    private readonly string twitterAdress = "https://twitter.com/intent/tweet";
    private readonly string levelUpGameJamLink = "https://andrew-raya.itch.io/scraplands";

    private Button shareButton;

    public void ShareTwitter()
    {
        Application.OpenURL(twitterAdress + "?text=" + UnityWebRequest.EscapeURL(twitterNameParameter + "\n" + twitterDescriptionParam + "\n" + levelUpGameJamLink));
    }
}

