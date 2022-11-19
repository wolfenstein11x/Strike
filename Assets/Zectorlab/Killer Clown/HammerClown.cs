using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HammerClown : MonoBehaviour
{
    [SerializeField] AudioSource footStep;
    [SerializeField] AudioSource hammerSmash;
    [SerializeField] Transform destination;

    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetComponent<Animator>().SetTrigger("walk");
        navMeshAgent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootStep()
    {
        footStep.Play();
    }

    public void PlayHammerSmash()
    {
        hammerSmash.Play();
    }
}
