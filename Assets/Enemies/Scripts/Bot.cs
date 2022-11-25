using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Soldier
{
    [SerializeField] AudioSource walkSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalkSound()
    {
        walkSound.Play();
    }
}
