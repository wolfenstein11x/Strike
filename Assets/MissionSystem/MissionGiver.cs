using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGiver : MonoBehaviour
{
    public Mission mission;
    
    PlayerStatus player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
    }

    public void GiveMission()
    {
        player.mission = mission;
        mission.isActive = true;

        Debug.Log("New mission: " + mission.title);
        Debug.Log(mission.description);
    }
}
