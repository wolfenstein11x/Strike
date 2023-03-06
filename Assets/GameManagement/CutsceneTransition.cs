using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentIndex + 1);
        
        
        
        
        SceneManager.LoadScene(0);
    }
}
