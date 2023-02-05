using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OneLiner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI oneLinerText;
    [SerializeField] string line;
    [SerializeField] int lettersPerSecond = 10;

    void Start()
    {
        //TypeLine();
    }

    private void OnEnable()
    {
        TypeLine();
    }

    private void OnDisable()
    {
        oneLinerText.text = "";
    }

    public void TypeLine()
    {
        StartCoroutine(TypeLineCoroutine());
    }

    IEnumerator TypeLineCoroutine()
    {
        oneLinerText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            oneLinerText.text += letter;

            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

}
