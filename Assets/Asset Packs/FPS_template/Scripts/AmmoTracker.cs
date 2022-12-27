using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTracker : MonoBehaviour
{
    [SerializeField] int maxAmmo = 30;
    [SerializeField] GameObject[] magazines;
    [SerializeField] int startingMagazines = 2;
    [SerializeField] AudioSource reloadSound;

    int currentAmmo;
    TextMeshProUGUI ammoText;
    int magazineIndex;

    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
        
        // TODO grab currentAmmo from somewhere else
        currentAmmo = maxAmmo;
        SetAmmoDisplay(currentAmmo, maxAmmo);

        InitializeMagazines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void DecrementAmmo()
    {
        currentAmmo--;
        SetAmmoDisplay(currentAmmo, maxAmmo);
    }

    private void SetAmmoDisplay(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }

    public int GetAmmoCount()
    {
        return currentAmmo;
    }

    void InitializeMagazines()
    {
        foreach(GameObject mag in magazines)
        {
            mag.SetActive(false);
        }

        for (int i=0; i < startingMagazines; i++)
        {
            magazines[i].SetActive(true);
        }

        magazineIndex = startingMagazines - 1;
    }

    void Reload()
    {
        // magazine index is at -1 when no mags left
        if (magazineIndex < 0) { return; }

        // play reload sound and update magazine index and GUI
        reloadSound.Play();
        magazines[magazineIndex].SetActive(false);
        magazineIndex--;

        // max ammo and display it on GUI
        currentAmmo = maxAmmo;
        SetAmmoDisplay(currentAmmo, maxAmmo);
    }
}
