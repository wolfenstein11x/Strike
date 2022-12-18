using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGiver : MonoBehaviour
{
    public Mission mission;
    
    PlayerStatus player;

    [SerializeField] GameObject[] missionItems;

    private void Start()
    {
        activateMissionItems(false);
        player = FindObjectOfType<PlayerStatus>();
    }

    public void GiveMission()
    {
        player.mission = mission;
        mission.isActive = true;

        Debug.Log("New mission: " + mission.title);
        Debug.Log(mission.description);

        activateMissionItems(true);
    }

    void activateMissionItems(bool status)
    {
        foreach (GameObject item in missionItems)
        {
            item.SetActive(status);
        }
    }
}
