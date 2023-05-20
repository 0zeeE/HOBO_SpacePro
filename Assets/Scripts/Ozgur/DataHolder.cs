using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance;

    StartBoost BoosterStat;
    RocketMovement RocketStat;
    private int earnedMoney;
    [SerializeField] private int rocketLevel = 1;
    [SerializeField] private int boosterLevel = 1;
    [SerializeField] private int fuelLevel = 1;

    private void Awake()
    {
        
    }
    void Start()
    {
        //Diger scene'e tasimak icin olusturulmus kod:
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);

        //Diger mapteki upgrade olayina gore upgrade'i tasiyan kod

        BoosterStat.boosterLevel = boosterLevel;
        RocketStat.rocketLevel = rocketLevel;
        RocketStat.fuelLevel = fuelLevel;

    }

    // Update is called once per frame
    void Update()
    {
                
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

    void IncreaseMoney()
    {
        earnedMoney = (int)RocketStat.maxHeight /100;
    }
}
