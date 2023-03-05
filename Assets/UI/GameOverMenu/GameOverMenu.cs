using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;

    int activeSceneIdx;

    void Start()
    {
        activeSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    public void PlayAgainButton()
    {
        clickSound.Play();
        SceneManager.LoadScene(activeSceneIdx);
    }



}
