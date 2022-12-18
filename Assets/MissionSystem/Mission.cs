using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission 
{
    [SerializeField] MissionGiver nextMission;

    public bool isActive;
    public string title;
    public string description;

    public MissionObjective objective;

    public void Complete()
    {
        isActive = false;

        Debug.Log("Mission: " + title + " was completed");

        if (nextMission != null)
        {
            nextMission.GiveMission();
        }

        else
        {
            Debug.Log("you win keeyud");
        }
    }
}
