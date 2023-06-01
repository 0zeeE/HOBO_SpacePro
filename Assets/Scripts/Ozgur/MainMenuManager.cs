using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //Ana menu
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CreditsCanvas;
    private bool isFullyLoaded = false;

    //Ogretici Kanvaslari
    [SerializeField] private GameObject Tutorial1;
    [SerializeField] private GameObject Tutorial2;
    [SerializeField] private GameObject Tutorial3;
    [SerializeField] private GameObject Tutorial4;



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

    public void TutorialMenu()
    {
        MainMenuCanvas.SetActive(false);
        Tutorial1.SetActive(true);
    }

    public void Tutorial1To2()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
    }

    public void Tutorial2To3()
    {
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(true);
    }

    public void Tutorial3To4()
    {
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(true);
    }

    public void CloseTutorial()
    {
        if(Tutorial1.activeInHierarchy == true)
        {
            Tutorial1.SetActive(false);
        }
        if (Tutorial2.activeInHierarchy == true)
        {
            Tutorial2.SetActive(false);
        }
        if (Tutorial3.activeInHierarchy == true)
        {
            Tutorial3.SetActive(false);
        }
        if (Tutorial4.activeInHierarchy == true)
        {
            Tutorial4.SetActive(false);
        }
        MainMenuCanvas.SetActive(true);


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
