using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    [SerializeField] float flightSpeed = 5f;
    [SerializeField] FlightPath flightPath;
    [SerializeField] float attackRange = 25f;
    [SerializeField] Transform target;
    [SerializeField] Bomb bomb;
    [SerializeField] Transform bombSpawnPoint;
    [SerializeField] float timeBetweenBombs = 1f;
    public bool airStrikeCalled = false;
    
    Vector3 destination;
    AudioSource jetSound;
    bool playedjetSound = false;

    bool readyToDropBomb = true;

    // Start is called before the first frame update
    void Start()
    {
        ResetPositions();

        jetSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Fly();

        if (ReachedDestination())
        {
            gameObject.SetActive(false);
        }

        if (InAttackRange())
        {
            PlayJetSound();
            DropBomb();
        }
    }


    void Fly()
    {
        if (airStrikeCalled)
        {
            var step = flightSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }

    private void OnEnable()
    {
        playedjetSound = false;
        ResetPositions();
    }

    void ResetPositions()
    {
        transform.position = flightPath.GetWaypoint(0);
        destination = GetDestination();
    }

    private Vector3 GetDestination()
    {
        return flightPath.GetWaypoint(1);
    }

    bool ReachedDestination()
    {
        return (Vector3.Distance(transform.position, destination) <= 1f);
    }

    bool InAttackRange()
    {
        float xPos = transform.position.x;
        float xPosTarget = target.position.x;

        return (Mathf.Abs(xPosTarget - xPos) <= attackRange);
    }

    void PlayJetSound()
    {
        if (playedjetSound) { return; }

        jetSound.Play();
        playedjetSound = true;
    }

    void DropBomb()
    {
        if (!readyToDropBomb) { return; }

        Instantiate(bomb, bombSpawnPoint);
        readyToDropBomb = false;

        Invoke("ResetBomb", timeBetweenBombs);
    }

    void ResetBomb()
    {
        readyToDropBomb = true;
    }
}
