using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTracker : MonoBehaviour
{
    [SerializeField] int maxAmmo = 30;

    int currentAmmo;
    TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
        
        // TODO grab currentAmmo from somewhere else
        currentAmmo = maxAmmo;
        SetAmmoDisplay(currentAmmo, maxAmmo);
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
}
