using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission 
{
    public bool isActive;
    public string title;
    public string description;

    public MissionObjective objective;

    public void Complete()
    {
        isActive = false;

        Debug.Log("Mission: " + title + " was completed");
    }
}
