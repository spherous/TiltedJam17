using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool Paused = false;
    public Canvas PauseScreen;

    public Button PauseExit;

    private void Start()
    {
        Button PauseExitButton = PauseExit.GetComponent<Button>();
        PauseExitButton.onClick.AddListener(ExitPause);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Paused);
        if ( Input.GetKeyDown(KeyCode.Escape) && Paused == false )
        {
            Paused = true;
            PauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if ( Input.GetKeyDown(KeyCode.Escape) && Paused == true )
        {
            Paused = false;
            PauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void ExitPause()
    {
        Time.timeScale = 1f;
        PauseScreen.gameObject.SetActive(false);
    }
}
