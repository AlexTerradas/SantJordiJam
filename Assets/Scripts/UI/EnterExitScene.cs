using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterExitScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup _blackCanvas;
    [SerializeField] private float _fadeDuration;
    [HideInInspector] public string _sceneName;
    private FadePanel _fade;
    
    void Start()
    {
        _fade = GetComponent<FadePanel>();
        _fade.StartCoroutine(_fade.Fade(_blackCanvas, 1, 0, 0, _fadeDuration));
    }

    public void FadeOutAndChangeScene(string sceneName)
    {
        _sceneName = sceneName;
        _fade.StartCoroutine(_fade.Fade(_blackCanvas, 0, 1, 0, _fadeDuration, ChangeScene));
    }
    
    public void ChangeScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_sceneName);
    }
}