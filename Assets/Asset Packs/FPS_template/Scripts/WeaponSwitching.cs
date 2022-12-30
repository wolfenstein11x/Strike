using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] int selectedWeapon = 0;
    [SerializeField] AudioSource weaponSwitchingSound;

    FlagTracker flagTracker;

    // Start is called before the first frame update
    void Start()
    {
        flagTracker = FindObjectOfType<FlagTracker>();

        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // ignore input if currently zoomed in
            if (flagTracker.ZoomedIn()) { return; }

            if (selectedWeapon >= transform.childCount - 1) { selectedWeapon = 0; }

            else { selectedWeapon++; }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // ignore input if currently zoomed in
            if (flagTracker.ZoomedIn()) { return; }

            if (selectedWeapon <= 0) { selectedWeapon = transform.childCount - 1; }

            else { selectedWeapon--; }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            weaponSwitchingSound.Play();
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    
}
