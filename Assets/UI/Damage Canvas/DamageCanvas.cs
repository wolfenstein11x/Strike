using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCanvas : MonoBehaviour
{
    [SerializeField] Blood[] bloods;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivateRandomBloodSplat();
        }
    }

    public void ActivateRandomBloodSplat()
    {
        int randomIndex = Random.Range(0, bloods.Length);

        if (bloods[randomIndex].isActiveAndEnabled)
        {
            //Debug.Log("already active");
            return;
        }

        else
        {
            bloods[randomIndex].gameObject.SetActive(true);
        }
    }
}
