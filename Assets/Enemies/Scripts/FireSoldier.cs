using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSoldier : Soldier
{
    [SerializeField] GameObject fireEffect;

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

    public bool InFiringRange()
    {
        return (Vector3.Distance(transform.position, player.transform.position) <= fireRange);
    }
}
