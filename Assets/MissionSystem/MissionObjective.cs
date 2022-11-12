using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionObjective
{
    public ObjectiveType objectiveType;
    public int requiredAmount;
    public int currentAmount;

    public bool ObjectiveReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled(string tag)
    {
        if (objectiveType == ObjectiveType.Kill)
        {
            currentAmount++;
        }
    }

    public void ItemCollected()
    {
        if (objectiveType == ObjectiveType.Gathering)
        {
            currentAmount++;
        }
    }
}

public enum ObjectiveType
{
    Kill,
    Gathering
}
