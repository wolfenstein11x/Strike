using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public Mission mission;

    public void RecordKill(string tag)
    {
        if (mission.isActive)
        {
            mission.objective.EnemyKilled(tag);
            
            if (mission.objective.ObjectiveReached())
            {
                mission.Complete();
            }
        }
    }

    
}
