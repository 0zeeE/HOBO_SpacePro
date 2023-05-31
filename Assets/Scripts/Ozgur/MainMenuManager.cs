using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CreditsCanvas;
    private bool isFullyLoaded = false;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Continue()
    {
       
        StartCoroutine(LoadScreen());
        StartCoroutine(LoadSection());

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        MainMenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void MainMenu()
    {
        MainMenuCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }
    
    IEnumerator LoadScreen()
    {
        isFullyLoaded = false;
        SceneManager.LoadScene("SampleScene");
        isFullyLoaded = true;
        yield return null;
    }

    IEnumerator LoadSection()
    {
        
        while (isFullyLoaded == false)//yükleme bekliyo
        {

            yield return null;

        }
        yield return new WaitForSeconds(1.5f);
        DilSaveLoaddo[] saveScr = GameObject.FindObjectsOfType<DilSaveLoaddo>();
        saveScr[0].LoadGame();
        Destroy(this.gameObject);


    }
}
