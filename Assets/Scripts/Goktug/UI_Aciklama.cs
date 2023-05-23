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
        Building[] Depomuz = GameObject.FindObjectsOfType<Building>();
        if (Building.AnyChosen == false)
        {
            panelll.SetActive(false);
            return;
        }
        panelll.SetActive(true);
        foreach (Building a in Depomuz)
        {
            if (a.amIChosen == true)
            {
                if(benBuyum == benNeyim.uretimAciklamasi)
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
    public void lvlUptutunulacakObj()
    {
        bunuTutuyorum.lvlUp();
    }
}
