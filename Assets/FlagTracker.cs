using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTracker : MonoBehaviour
{
    bool gamePaused;

    public void SetGamePaused(bool status)
    {
        gamePaused = status;
    }

    public bool GamePaused()
    {
        return gamePaused;
    }
}
