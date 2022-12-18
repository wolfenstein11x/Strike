using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public Mission mission;

    MissionText missionText;

    private void Start()
    {
        missionText = FindObjectOfType<MissionText>();
    }

    void SetObjectiveCompleteText()
    {
        TextMeshProUGUI currentMissionText = missionText.GetComponent<TextMeshProUGUI>();
        currentMissionText.text = "Objective complete";
    }

    public void RecordKill(string tag)
    {
        if (mission.isActive)
        {
            mission.objective.EnemyKilled(tag);
            
            if (mission.objective.ObjectiveReached())
            {
                mission.Complete();
                SetObjectiveCompleteText();
            }
        }
    }

    public void RecordItemCollected(string tag)
    {
        if (mission.isActive)
        {
            mission.objective.ItemCollected(tag);

            if (mission.objective.ObjectiveReached())
            {
                mission.Complete();
                SetObjectiveCompleteText();
            }
        }
    }

    public void RecordArrivedAtPlace(string tag)
    {
        if (mission.isActive)
        {
            if (mission.objective.DestinationReached(tag))
            {
                mission.Complete();
                SetObjectiveCompleteText();
            }
        }
    }

    
}
