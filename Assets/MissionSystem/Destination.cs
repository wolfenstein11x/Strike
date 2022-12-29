using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] GameObject minimapIcon;

    PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            minimapIcon.SetActive(false);
            playerStatus.RecordArrivedAtPlace(gameObject.tag);
        }
    }
}
