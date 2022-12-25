using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    void Start()
    {
        // de-child from plane so it doesn't move with plane
        transform.parent = null;

        
        //RandomizeFall();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("explode!");
    }

    
}
