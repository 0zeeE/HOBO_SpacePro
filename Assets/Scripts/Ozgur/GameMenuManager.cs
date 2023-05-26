using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameMenuManager : MonoBehaviour
{
    public static bool isGamePaused = false;
   

    [SerializeField] private GameObject Rocket;
    [SerializeField] private GameObject Dataholder;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject endMenu;

    [SerializeField] private TMP_Text attitudeText;
    [SerializeField] private TMP_Text earnedMoneyText;
    private float attitude;
    private int money;


    // Start is called before the first frame update


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Dataholder == null)
        {
            Dataholder = GameObject.FindGameObjectWithTag("DataHolder");
        }


        attitude = (int)Rocket.GetComponent<RocketMovement>().maxHeight;
        money = Dataholder.GetComponent<DataHolder>().GetMoney();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
            

        }
    }

    void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void RestartSection()
    {
        Debug.Log("Restarting");
        Resume();
        StartCoroutine(loadThis(SceneManager.GetActiveScene().name));
    }

    static public bool isFullyLoaded;
    IEnumerator loadThis(string sceneName)
    {
        isFullyLoaded = false;
        SceneManager.LoadScene(sceneName);
        isFullyLoaded = true;

        yield return null;

    }

    public void ExitGame()
    {
        Resume();
        Debug.Log("Exiting the game");
        Application.Quit();
    }

    public void ReturnMap()
    {
        Debug.Log("Returning Map");
        Resume();
        StartCoroutine(loadThis("SampleScene"));
        DataHolder[] dataHolder = GameObject.FindObjectsOfType<DataHolder>();
        dataHolder[0].burasi();
    }


    public void EndFlight()
    {
        endMenu.SetActive(true);
        menu.SetActive(false);
        Time.timeScale = 0f;
        attitudeText.text = attitude.ToString() +" metre";
        earnedMoneyText.text = money.ToString() + " TL";
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
