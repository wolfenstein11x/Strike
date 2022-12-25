using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    [SerializeField] float flightSpeed = 5f;
    [SerializeField] FlightPath flightPath;
    [SerializeField] float attackRange = 25f;
    [SerializeField] Bomb bomb;
    [SerializeField] Transform bombSpawnPoint;
    [SerializeField] float timeBetweenBombs = 1f;

    Vector3 destination;
    PlayerStatus player;
    AudioSource jetSound;
    bool playedjetSound = false;

    bool allowInvoke = true;
    bool readyToDropBomb = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = flightPath.GetWaypoint(0);

        destination = GetDestination();

        player = FindObjectOfType<PlayerStatus>();

        jetSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var step = flightSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (ReachedDestination())
        {
            Destroy(gameObject);
        }

        if (InAttackRange())
        {
            PlayJetSound();
            DropBomb();
        }
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
        float xPosPlayer = player.transform.position.x;

        return (Mathf.Abs(xPosPlayer - xPos) <= attackRange);
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
