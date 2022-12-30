using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTracker : MonoBehaviour
{
    bool gamePaused;
    bool playerDead;
    bool zoomedIn;

    public void SetGamePaused(bool status)
    {
        gamePaused = status;
    }

    public bool GamePaused()
    {
        return gamePaused;
    }

    public void SetPlayerDead(bool status)
    {
        playerDead = true;
    }

    public bool PlayerDead()
    {
        return playerDead;
    }

    public void SetZoomedIn(bool status)
    {
        zoomedIn = status;
    }

    public bool ZoomedIn()
    {
        return zoomedIn;
    }
}
