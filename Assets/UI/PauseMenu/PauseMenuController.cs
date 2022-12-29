using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject minimap;
    [SerializeField] GameObject fullMap;

    FlagTracker flagTracker;

    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        flagTracker = FindObjectOfType<FlagTracker>();
        flagTracker.SetGamePaused(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    void Pause()
    {
        flagTracker.SetGamePaused(true);

        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        ShowMinimap(false);
        ShowFullMap(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        ShowMinimap(true);
        ShowFullMap(false);
        Time.timeScale = 1;

        flagTracker.SetGamePaused(false);
    }

    void ShowMinimap(bool status)
    {
        minimap.SetActive(status);
    }

    void ShowFullMap(bool status)
    {
        fullMap.SetActive(status);
    }

    
}
