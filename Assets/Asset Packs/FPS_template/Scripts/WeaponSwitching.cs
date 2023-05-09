using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
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

            selectedWeapon = IncrementWeapon(selectedWeapon);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // ignore input if currently zoomed in
            if (flagTracker.ZoomedIn()) { return; }

            selectedWeapon = DecrementWeapon(selectedWeapon);
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            weaponSwitchingSound.Play();
            SelectWeapon();
        }
    }

    private int IncrementWeapon(int selectedWeaponIn)
    {
        bool validSelection = false;
        int selectedWeaponOut = 0;

        while (!validSelection)
        {
            if (selectedWeaponIn >= transform.childCount - 1) { selectedWeaponIn = 0; }

            else { selectedWeaponIn++; }

            bool selectedWeaponObtained = weapons[selectedWeaponIn].obtained;
            validSelection = selectedWeaponObtained;
        }

        selectedWeaponOut = selectedWeaponIn;
        return selectedWeaponOut;
    }

    private int DecrementWeapon(int selectedWeaponIn)
    {
        bool validSelection = false;
        int selectedWeaponOut = 0;

        while (!validSelection)
        {
            if (selectedWeaponIn <= 0) { selectedWeaponIn = transform.childCount - 1; }

            else { selectedWeaponIn--; }

            bool selectedWeaponObtained = weapons[selectedWeaponIn].obtained;
            validSelection = selectedWeaponObtained;
        }

        selectedWeaponOut = selectedWeaponIn;
        return selectedWeaponOut;
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
