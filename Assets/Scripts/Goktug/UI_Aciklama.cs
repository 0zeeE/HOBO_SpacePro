using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UI_Aciklama : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI aciklamam;
    [SerializeField] private GameObject panelll;
    [SerializeField] private benNeyim benBuyum;
    [SerializeField] private Building bunuTutuyorum;
    [SerializeField] private ShipParts bunuTutuyorumShipParts;
    public enum benNeyim{
        uretimAciklamasi,
        lvlGereksinimi
    };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tutun();
    }
    private void tutun()
    {

        if (Building.AnyChosen == false && ShipParts.AnyChosen == false)
        {
            panelll.SetActive(false);
            return;
        }
        panelll.SetActive(true);
        if (Building.AnyChosen == true)
        {
            Building[] binamiz = GameObject.FindObjectsOfType<Building>();
            foreach (Building a in binamiz)
            {
                if (a.amIChosen == true)
                {
                    if (benBuyum == benNeyim.uretimAciklamasi)
                    {
                        bunaTutunUretimAciklama(a);
                    }
                    if (benBuyum == benNeyim.lvlGereksinimi)
                    {
                        bunaTutunLvl(a);
                    }
                    bunuTutuyorum = a;
                    return;
                }
            }
        }


        if (ShipParts.AnyChosen == true)
        {
            ShipParts[] shipimiz = GameObject.FindObjectsOfType<ShipParts>();
            foreach (ShipParts a in shipimiz)
            {
                if (a.amIChosen == true)
                {
                    if (benBuyum == benNeyim.uretimAciklamasi)
                    {
                        bunaTutunUretimAciklama(a);
                    }
                    if (benBuyum == benNeyim.lvlGereksinimi)
                    {
                        bunaTutunLvl(a);
                    }
                    bunuTutuyorumShipParts = a;
                    return;
                }
            }
        }

    }
    private void bunaTutunUretimAciklama(Building tutunulacakObj)
    {
        string aciklama="";
        foreach (myMaterialHolder kullan in tutunulacakObj.consume)
        {
            aciklama += kullan.amountt +"x"+ kullan.myMateriall.name +  " ";
        }
        aciklama += " -> ";

        foreach (myMaterialHolder produce in tutunulacakObj.produce)
        {
            aciklama += produce.amountt * tutunulacakObj.myLvl + "x"+ produce.myMateriall.name + " ";
        }
        aciklama += " /"+tutunulacakObj.produceTimeMax + "sn";
        aciklamam.text = aciklama;

        //Debug.Log(aciklama);
    }

    private void bunaTutunLvl(Building tutunulacakObj)
    {
        string aciklama = "";

        aciklama += tutunulacakObj.Exp + "/" + tutunulacakObj.gerekliExpNow + " exp\n";

        foreach (myMaterialHolder kullan in tutunulacakObj.lvlUpRequ)
        {
            aciklama += kullan.amountt* tutunulacakObj.myLvl + "x" + kullan.myMateriall.name + " ";
        }
        aciklama += " -> seviye++";


        aciklamam.text = aciklama;

        //Debug.Log(aciklama);
    }



    private void bunaTutunUretimAciklama(ShipParts tutunulacakObj)
    {
        string aciklama = "";
        /*
        foreach (myMaterialHolder kullan in tutunulacakObj.consume)
        {
            aciklama += kullan.amountt + "x" + kullan.myMateriall.name + " ";
        }
        aciklama += " -> ";

        foreach (myMaterialHolder produce in tutunulacakObj.produce)
        {
            aciklama += produce.amountt * tutunulacakObj.myLvl + "x" + produce.myMateriall.name + " ";
        }
        aciklama += " /" + tutunulacakObj.produceTimeMax + "sn";
        */
        aciklama += tutunulacakObj.myAd + "\nlvl: " + tutunulacakObj.myLvl + "\n"+ tutunulacakObj.myDes;
        aciklamam.text = aciklama;

        //Debug.Log(aciklama);
    }
    private void bunaTutunLvl(ShipParts tutunulacakObj)
    {
        string aciklama = "";
        
       

        foreach (myMaterialHolder kullan in tutunulacakObj.lvlUpRequ)
        {
            if (tutunulacakObj.myLvl == 0)
            {
                aciklama += kullan.amountt + "x" + kullan.myMateriall.name + " ";
            }
            else
            {
                aciklama += kullan.amountt * tutunulacakObj.myLvl + "x" + kullan.myMateriall.name + " ";
            }
            
        }
        
        aciklama += " -> seviye++";


        aciklamam.text = aciklama;
    }

    public void lvlUptutunulacakObj()
    {
        if (Building.AnyChosen == true)
        {
            bunuTutuyorum.lvlUp();
            return;
        }
        if (ShipParts.AnyChosen == true)
        {
            bunuTutuyorumShipParts.lvlUp();
            return;
        }
        
    }
}
