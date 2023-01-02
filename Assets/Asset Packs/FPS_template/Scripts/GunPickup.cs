using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] AudioSource pickupSound;
    [SerializeField] GameObject playerGun;
    [SerializeField] GameObject mapIcon;

    PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>();

        if (mapIcon != null)
        {
            mapIcon.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pickupSound.Play();
            player.RecordItemCollected(gameObject.tag);
            //playerGun.SetActive(true);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            mapIcon.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
}
