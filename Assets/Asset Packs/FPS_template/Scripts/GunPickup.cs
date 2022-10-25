using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] AudioSource pickupSound;
    [SerializeField] GameObject playerGun;

    // Start is called before the first frame update
    void Start()
    {
        
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
            playerGun.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
