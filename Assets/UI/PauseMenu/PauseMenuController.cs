using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    bool isPaused;

    FlagTracker flagTracker;

    void Awake()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void Start()
    {
        flagTracker = FindObjectOfType<FlagTracker>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            Unpause();
        }

        else
        {
            Pause();
        }
    }

    void Pause()
    {
        flagTracker.SetGamePaused(true);

        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        flagTracker.SetGamePaused(false);
    }
}
