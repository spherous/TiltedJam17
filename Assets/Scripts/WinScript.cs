using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public Button Home;
    public Canvas WinCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Button HomeButton = Home.GetComponent<Button>();
        HomeButton.onClick.AddListener(GoHome);
    }

    IEnumerator LoadingScene(string scene)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(scene);
    }

    void Win()
    {
        WinCanvas.gameObject.SetActive(true);
        gameObject.GetComponent<GameOver>().Dead = true;
    }

    void GoHome()
    {
        StartCoroutine(LoadingScene("TitleScene"));
    }
}
