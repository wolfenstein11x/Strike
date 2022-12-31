using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slerpin : MonoBehaviour
{
    [SerializeField] float recoilAmount = 100f;

    Rigidbody body;
    PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerStatus>();
        body = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Recoil();
        }
    }

    

    void Recoil()
    {
        float xForce = Random.Range(-1.0f, 1.0f) * recoilAmount;
        float yForce = Random.Range(-1.0f, 1.0f) * recoilAmount;
        float zForce = Random.Range(-1.0f, 1.0f) * recoilAmount;



        Vector3 forceVector = new Vector3(xForce, yForce, zForce);

        body.AddForce(forceVector, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(forceVector);


    }

}
