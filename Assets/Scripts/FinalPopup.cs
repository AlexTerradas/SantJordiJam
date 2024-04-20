using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPopup : MonoBehaviour
{
    private EnterExitScene enterExitScene;
    [SerializeField] private Slider _scoreSlider;
    [SerializeField] private float duration;

    private void Start()
    {
        enterExitScene = FindObjectOfType<EnterExitScene>();
        _scoreSlider.maxValue = GameManager.instance.maxScore;
        StartCoroutine(SetSlider());
    }

    public void MainMenu()
    {
        enterExitScene.FadeOutAndChangeScene("MainMenu");
    }

    public void Continue()
    {
        enterExitScene.FadeOutAndChangeScene("EndCinematicScene");
    }

    public void Restart()
    {
        enterExitScene.FadeOutAndChangeScene(SceneManager.GetActiveScene().ToString());
    }
    
    public void ButtonClick()
    {
        AudioManager.instance.PlayOneShot(AudioManager.instance.ButtonClick);
    }
    
    public IEnumerator SetSlider ()
    {
        yield return new WaitForSeconds(0.5f);
        
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.unscaledDeltaTime;
            _scoreSlider.value = Mathf.Lerp(0, GameManager.instance.playerScore, counter / duration);
            yield return null;
        }
    }
}
