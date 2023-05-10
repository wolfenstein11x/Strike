using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : OneLiner
{
    [SerializeField] float timeActive = 3f;

    private void OnEnable()
    {
        TypeLine();
        Invoke("DisableSelf", timeActive);
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
