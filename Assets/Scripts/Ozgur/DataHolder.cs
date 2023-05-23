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

    private void Awake()
    {
        
    }
    private void adamOl()
    {
        Booster.GetComponent<StartBoost>().boosterLevel = boosterLevel;
        Booster.GetComponent<StartBoost>().SetBoosterLevel();
        Rocket.GetComponent<RocketMovement>().rocketLevel = rocketLevel;
        Rocket.GetComponent<RocketMovement>().SetRocketLevel();
        Rocket.GetComponent<RocketMovement>().fuelLevel = fuelLevel;
        Rocket.GetComponent<RocketMovement>().SetFuelLevel();
    }
    void Start()
    {
        




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


        IncreaseMoney();

        
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
