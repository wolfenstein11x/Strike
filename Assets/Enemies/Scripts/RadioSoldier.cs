using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSoldier : Soldier
{
    [SerializeField] float timeBetweenStrikesMin = 10f;
    [SerializeField] float timeBetweenStrikesMax = 20f;
    [SerializeField] float airStrikeDuration = 10f;

    Aircraft[] bombers;
    EnemyHealth health;

    float timer;
    float nextStrikeTime;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        health = GetComponent<EnemyHealth>();

        bombers = FindObjectsOfType<Aircraft>();
        SetBombers(false);
        ResetAirStrikeTimer();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead()) { return; }

        if (timer >= nextStrikeTime)
        {
            CallAirStrike();
        }

        else
        {
            timer += Time.deltaTime;
        }
    }

    void CallAirStrike()
    {
        //Debug.Log("strike incoming!");
        SetBombers(false);
        SetBombers(true);
        ResetAirStrikeTimer();
    }

    void SetBombers(bool status)
    {
        foreach (Aircraft bomber in bombers)
        {
            bomber.gameObject.SetActive(status);
            bomber.airStrikeCalled = status;
        }
    }

    void ResetAirStrikeTimer()
    {
        timer = 0;
        nextStrikeTime = Random.Range(timeBetweenStrikesMin, timeBetweenStrikesMax);
    }
}
