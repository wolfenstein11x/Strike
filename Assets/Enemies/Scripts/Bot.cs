using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Soldier
{
    [SerializeField] AudioSource walkSound;
    [SerializeField] ParticleSystem[] muzzleFlashes;
    [SerializeField] Transform[] projectileSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // here for debugging, delete later
        FaceTarget();
    }

    public void PlayWalkSound()
    {
        walkSound.Play();
    }

    public void FireGun(int gunIndex)
    {
        gunSound.Play();
        muzzleFlashes[gunIndex].Play();
        muzzleFlashes[gunIndex + 1].Play();
    }
}
