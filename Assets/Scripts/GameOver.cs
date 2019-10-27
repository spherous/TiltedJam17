using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI EndWords;
    public TextMeshProUGUI SurvivedTime;
    public Button Resume;
    public Button Quit;

    public Canvas GameOverCanvas;

    public bool Dead = false;


    //"Date: Unknown  |  Year: 1900\nSurvived: "+ (int)time/60 +"Minutes and "+ (int)time + " Seconds";

    // Start is called before the first frame update
    void Start()
    {
        Button ResumeButton = Resume.GetComponent<Button>();
        ResumeButton.onClick.AddListener(TryAgain);
        Button QuitButton = Quit.GetComponent<Button>();
        QuitButton.onClick.AddListener(MainMenu);
    }

    void EndGame()
    {
        GameOverCanvas.gameObject.SetActive(true);
        Dead = true;
        StartCoroutine(FadeTextIn(5f, EndWords));
    }

    IEnumerator FadeTextIn(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        int time = (int)(gameObject.GetComponent<Pause>().getTime() - t);
        SurvivedTime.text = "Survived: " + time / 60 + " Minutes and " + time + " Seconds";
        Resume.gameObject.SetActive(true);
        Quit.gameObject.SetActive(true);
        SurvivedTime.gameObject.SetActive(true);
    }

    IEnumerator LoadingScene(string scene)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(scene);
    }

    void TryAgain()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadingScene("MainScene"));
    }

    void MainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadingScene("TitleScene"));
    }
}
