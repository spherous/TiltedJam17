using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Pause : MonoBehaviour
{
    bool Paused = false;
    public Canvas PauseScreen;

    public Button PauseExit;
    public Button Title;
    public TextMeshProUGUI TimeDisplay;

    public float time;

    private void Start()
    {
        Button TitleButton = Title.GetComponent<Button>();
        TitleButton.onClick.AddListener(ToTitle);
        Button PauseExitButton = PauseExit.GetComponent<Button>();
        PauseExitButton.onClick.AddListener(ExitPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Paused == false)
        {
            time += Time.deltaTime;
        }
        else if (Paused == true || gameObject.GetComponent<GameOver>().Dead == true )
        {
            time += Time.deltaTime * 0;
        }

        TimeDisplay.text = "Date: Unknown  |  Year: 1900\nSurvived: "+ (int)time/60 +"Minutes and "+ (int)time + " Seconds";

        if ( Input.GetKeyDown(KeyCode.Escape) && Paused == false && gameObject.GetComponent<GameOver>().Dead == false)
        {
            Paused = true;
            PauseScreen.gameObject.SetActive(true);
        }
        else if ( Input.GetKeyDown(KeyCode.Escape) && Paused == true && gameObject.GetComponent<GameOver>().Dead == false)
        {
            Paused = false;
            PauseScreen.gameObject.SetActive(false);
        }
    }

    public float getTime()
    {
        return time;
    }


    void ToTitle()
    {
        StartCoroutine(LoadingScene());
    }

    void ExitPause()
    {
        Time.timeScale = 1f;
        PauseScreen.gameObject.SetActive(false);
    }
    IEnumerator LoadingScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("TitleScene");
    }
}
