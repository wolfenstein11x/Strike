using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] MissionGiver missionGiver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            missionGiver.GiveMission();
            Destroy(gameObject, 1f);
        }
    }
}
