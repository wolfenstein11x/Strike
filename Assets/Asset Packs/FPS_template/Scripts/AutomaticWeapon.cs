using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {

    }
}
