using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    static public bool isFullyLoaded;
    IEnumerator loadThis(string sceneName)
    {
        isFullyLoaded = false;
        SceneManager.LoadScene(sceneName);
        isFullyLoaded = true;

        yield return null;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
