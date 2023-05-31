using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mySceneManager : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    public static bool isGamePaused = false;
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
    


    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }



        }
    }

    void OpenMenu()
    {
        MainMenu.SetActive(false);
        isGamePaused = false;
    }

    void CloseMenu()
    {
        MainMenu.SetActive(true);
        isGamePaused = true;
    }

    




}
