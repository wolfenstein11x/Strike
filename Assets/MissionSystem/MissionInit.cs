using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInit : MonoBehaviour
{
    [SerializeField] MissionGiver missionGiver;
    [SerializeField] float delayTime = 3f;

    void Start()
    {
        Invoke("InitMission", delayTime);
    }

    void Update()
    {
        
    }

    void InitMission()
    {
        missionGiver.GiveMission();
    }
}
