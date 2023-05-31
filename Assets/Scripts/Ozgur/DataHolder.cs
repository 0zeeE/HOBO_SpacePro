using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance;

    [SerializeField] private GameObject Booster;
    [SerializeField] private GameObject Rocket;
    [SerializeField] private int rocketLevel = 1;
    [SerializeField] private int boosterLevel = 1;
    [SerializeField] private int fuelLevel = 1;
    [SerializeField] private int earnedMoney;
    [SerializeField] private myMaterialHolder para;
    private void Awake()
    {
        StartCoroutine(adamOl());
        SetRocketAndBoost();
        
    }
    
    IEnumerator adamOl()
    {
        
        while (GameMenuManager.isFullyLoaded==false)//yükleme bekliyo
        {
            
            yield return null;
           
        }
        yield return new WaitForSeconds(1.5f);

        Rocket.GetComponent<RocketMovement>().setMyLvls(rocketLevel, fuelLevel);


        
        Booster.GetComponent<StartBoost>().SetBoosterLvl(boosterLevel);


        try
        {
            Destroy(GameObject.Find("loadSc"));
        }
        catch
        {
            
        }
        

    }
    public void burasi()
    {
        StartCoroutine(topladiðimiAktar());
    }
    IEnumerator topladiðimiAktar()
    {

        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        Inventory[] Depomuz = GameObject.FindObjectsOfType<Inventory>();

        Depomuz[0].depoyaEkle(para, earnedMoney);
        Building[] builds = GameObject.FindObjectsOfType<Building>();
        foreach(Building bui in builds)
        {
            bui.loadMe();
        }
        Destroy(this.gameObject);
    }
    void Start()
    {


        //SetRocketAndBoost();
        //adamOl();




        //Diger scene'e tasimak icin olusturulmus kod:
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);

        //Diger mapteki upgrade olayina gore upgrade'i tasiyan kod

        

    }

    // Update is called once per frame
    void Update()
    {
        SetRocketAndBoost();


        
        adamOl();

        statGuncelle();

    }

    private void FixedUpdate()
    {
        IncreaseMoney();
    }
    public void statGuncelle()
    {
        try
        {

            rocketLevel = ShipParts4DataHolder.rocketLevel;
            boosterLevel = ShipParts4DataHolder.boosterLevel;
            fuelLevel = ShipParts4DataHolder.fuelLevel;

            adamOl();
        }
        catch
        {
            Debug.Log("patates burasý");

            return;
        }
    }
    public void IncreaseRocketLevel()
    {
        if(rocketLevel < 3)
        {
            rocketLevel++;
        }
        else
        {
            Debug.Log("Rocket en yuksek seviye");
        }
    }

    public void IncreaseBoosterLevel()
    {
        if(boosterLevel < 3)
        {
            boosterLevel++;
        }
        else
        {
            Debug.Log("Booster en yuksek seviye");
        }
    }

    public void IncreaseFuelLevel()
    {
        if(fuelLevel < 3)
        {
            fuelLevel++;
        }
        else
        {
            Debug.Log("Yakit deposu en yuksek seviye");
        }
    }

    public void IncreaseMoney()
    {
        if(Rocket != null)
        {
            earnedMoney = (int)Rocket.GetComponent<RocketMovement>().maxHeight / 10;
        }
        
    }

    public int GetMoney()
    {
        return earnedMoney;
    }

    public void SetRocketAndBoost()
    {
        if (Booster == null)
        {
            Booster = GameObject.FindGameObjectWithTag("Booster");
            adamOl();
        }
        if (Rocket == null)
        {
            Rocket = GameObject.FindGameObjectWithTag("Rocket");
            adamOl();
        }
    }
}
