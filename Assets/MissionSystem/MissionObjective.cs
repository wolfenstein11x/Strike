using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionObjective
{
    public ObjectiveType objectiveType;
    public string objectiveTag;
    public int requiredAmount;
    public int currentAmount;

    public bool ObjectiveReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public bool DestinationReached(string tag)
    {
        return objectiveTag == tag;
    }

    public void EnemyKilled(string tag)
    {
        if (objectiveTag != "none" && tag != objectiveTag) 
        {
            //Debug.Log("ya killed the wrong gah, gah");
            return; 
        }

        if (objectiveType == ObjectiveType.Kill)
        {
            currentAmount++;
        }
    }

    public void ItemCollected(string tag)
    {
        if (objectiveTag != "none" && tag != objectiveTag)
        {
            //Debug.Log("ya picked up the wrong thing, gah");
            return;
        }

        if (objectiveType == ObjectiveType.Gathering)
        {
            currentAmount++;
        }
    } 
}

public enum ObjectiveType
{
    Kill,
    Gathering,
    GoTo
}
