using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Button exitButton;
    public Button sceneButton1;
    public Button sceneButton2;
    public Button sceneButton3;
    public Button sceneButton4;
    public Button hanyangiButton;
    public AudioSource hanyangiAudio;
    public GameObject speechBubble;

    private Coroutine speechCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitButton.onClick.AddListener(QuitApp);
        sceneButton1.onClick.AddListener(() => LoadScene("Scene0_B1"));
        sceneButton2.onClick.AddListener(() => LoadScene("Scene0_B2"));
        sceneButton3.onClick.AddListener(() => LoadScene("Scene0_B3"));
        sceneButton4.onClick.AddListener(() => LoadScene("Scene0_B4"));
        hanyangiButton.onClick.AddListener(PlayHanyangiSpeech);
        speechBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void PlayHanyangiSpeech()
    {
        if (hanyangiAudio != null)
        {
            hanyangiAudio.Stop();
            hanyangiAudio.Play();
        }

        if (speechCoroutine != null)
        {
            StopCoroutine(speechCoroutine);
        }
        speechCoroutine = StartCoroutine(ShowSpeechBubble());
    }

    IEnumerator ShowSpeechBubble()
    {
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(8f);
        speechBubble.SetActive(false);
    }
}
