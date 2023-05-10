using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission 
{
    [SerializeField] MissionGiver nextMission;
    [SerializeField] GameObject postMissionDialogue;

    public bool isActive;
    public string title;
    public string description;

    public MissionObjective objective;

    public void Complete()
    {
        isActive = false;

        //Debug.Log("Objective: " + title + " was completed");

        if (nextMission != null)
        {
            PlayDialogue();
            nextMission.GiveMission();
        }

        else
        {
            Debug.Log("you win keeyud");
        }
    }

    private void PlayDialogue()
    {
        if (postMissionDialogue != null)
        {
            postMissionDialogue.SetActive(true);
        }
    }

    private void EndDialogue()
    {
        postMissionDialogue.SetActive(false);
    }
}
