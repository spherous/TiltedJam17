using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour { 

    public Button Play;
    public Button Credit;

    public Canvas CreditCanvas;
    public Button CreditClose;

    void Start()
    {
        Button PlayButton = Play.GetComponent<Button>();
        Button CreditButton = Credit.GetComponent<Button>();
        PlayButton.onClick.AddListener(StartGame);
        CreditButton.onClick.AddListener(CreditScreen);

        Button Exit = CreditClose.GetComponent<Button>();
        Exit.onClick.AddListener(CloseCredit);
    }

    void CloseCredit()
    {
        Debug.Log("Close the Credits");
        CreditCanvas.gameObject.SetActive(false);
    }

    void CreditScreen()
    {
        Debug.Log("Queue the Credits");
        CreditCanvas.gameObject.SetActive(true);
    }

    void StartGame()
    {
        Debug.Log("STARTOOO, Hajimariyo!");
        StartCoroutine(LoadingScene("MainScene"));
    }
    
    IEnumerator LoadingScene(string scene)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(scene);
    }
}
