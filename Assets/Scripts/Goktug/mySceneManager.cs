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
    public void toLaunch()
    {
        StartCoroutine(loadThis("Ozgur"));
    }
    static public bool isFullyLoaded;
    IEnumerator loadThis(string sceneName)
    {
        DilSaveLoaddo[] saveScr = GameObject.FindObjectsOfType<DilSaveLoaddo>();
        saveScr[0].saveGame();

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
