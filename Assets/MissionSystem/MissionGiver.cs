using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionGiver : MonoBehaviour
{
    public Mission mission;
    
    PlayerStatus player;
    MissionText missionText;

    [SerializeField] GameObject[] missionItems;
    [SerializeField] float delayTime = 3f;

    private void Start()
    {
        activateMissionItems(false);
        player = FindObjectOfType<PlayerStatus>();
        missionText = FindObjectOfType<MissionText>();
    }

    public void GiveMission()
    {
        StartCoroutine(GiveMissionCoroutine());
        /*
        player.mission = mission;
        mission.isActive = true;

        Debug.Log("New mission: " + mission.title);
        Debug.Log(mission.description);

        activateMissionItems(true);
        */
    }

    IEnumerator GiveMissionCoroutine()
    {
        yield return new WaitForSeconds(delayTime);

        player.mission = mission;
        mission.isActive = true;

        SetMissionText();
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

    void SetMissionText()
    {
        TextMeshProUGUI currentMissionText = missionText.GetComponent<TextMeshProUGUI>();
        currentMissionText.text = "Objective: " + mission.description;
    }
}
