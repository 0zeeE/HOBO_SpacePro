using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UI_Aciklama : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI aciklamam;
    [SerializeField] private GameObject panelll;
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
                bunaTutun(a);
                return;
            }
        }
    }
    private void bunaTutun(Building tutunulacakObj)
    {
        string aciklama="";
        foreach (myMaterialHolder kullan in tutunulacakObj.consume)
        {
            aciklama += kullan.amountt +"x"+ kullan.myMateriall.name +  " ";
        }
        aciklama += " -> ";

        foreach (myMaterialHolder produce in tutunulacakObj.produce)
        {
            aciklama += produce.amountt +"x"+ produce.myMateriall.name + " ";
        }
        aciklamam.text = aciklama;

        Debug.Log(aciklama);
    }
}
