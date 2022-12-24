using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    [SerializeField] float fadeRate = 0.01f;

    Image image;
    Color color; 

    private void OnEnable()
    {
        image = GetComponent<Image>();
        color = image.color;
        RefillAlpha();

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (float alpha = 1f; alpha >= 0; alpha -= fadeRate)
        {
            color.a = alpha;
            image.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.SetActive(false);
    }

    void RefillAlpha()
    {
        color.a = 1f;
        image.color = color;
    }
}
