using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSoldier : Soldier
{
    [SerializeField] GameObject fireEffect;
    [SerializeField] float fireRangeBuffer = 5f;
    [SerializeField] float maxWeaponDamage = 25f;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        fireEffect.SetActive(false);
        base.Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFlamethrower(bool status)
    {
        fireEffect.SetActive(status);
    }

    public void PlayFlamethrowerSound()
    {
        gunSound.Play();
    }

    public bool InFiringRange(bool plusBuffer=false)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("distance: " + distanceToPlayer);

        if (plusBuffer)
        {
            return (Vector3.Distance(transform.position, player.transform.position) <= (fireRange + fireRangeBuffer));
        }

        else
        {
            return (Vector3.Distance(transform.position, player.transform.position) <= fireRange);
        }
    }

    public void DealDamage()
    {
        Vector3 startPoint = projectileSpawnPoint.position;
        Vector3 endPoint = player.transform.position;

        Vector3 dir = (endPoint - startPoint).normalized;

        float damage = CalculateDamage(startPoint, endPoint);
        int damageInt = (int)damage;

        RaycastHit hit;
        if (Physics.Raycast(startPoint, dir, out hit, fireRange))
        {
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player == null) return;
            player.TakeDamage(damageInt);
        }
    }

    float CalculateDamage(Vector3 pos, Vector3 playerPos)
    {
        float distanceToPlayer = Vector3.Distance(pos, playerPos);

        float damage = maxWeaponDamage / distanceToPlayer;

        return damage;
    }
}
