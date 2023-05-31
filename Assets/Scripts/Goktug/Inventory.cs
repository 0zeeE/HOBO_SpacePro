using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] public myMaterialHolder[] sahiplerim;
    [SerializeField] private TextMeshProUGUI depodakilerim;

    public static Inventory Instance;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (depodakilerim == null)
        {
            depodakilerim = GameObject.Find("depo_icindekiler").GetComponent<TextMeshProUGUI>();
        }
    }
    public bool depodaVarMi(myMaterialHolder myMaterialHolderrr)
    {
        foreach(myMaterialHolder sahip in sahiplerim)
        {
            if (sahip.myMateriall == myMaterialHolderrr.myMateriall)
            {
                if (sahip.amountt >= myMaterialHolderrr.amountt)
                {
                    return true;
                }
            }
            
        }
        return false;
    }
    public void depodanCikar(myMaterialHolder myMaterialHolderrr)
    {
        foreach (myMaterialHolder sahip in sahiplerim)
        {
            if (sahip.myMateriall == myMaterialHolderrr.myMateriall)
            {
                if (sahip.amountt >= myMaterialHolderrr.amountt)
                {
                    sahip.amountt -= myMaterialHolderrr.amountt;
                    depoYaziGuncelle();
                    return;
                }
            }

        }
        depoYaziGuncelle();
    }
    public void depoyaEkle(myMaterialHolder myMaterialHolderrr)
    {
        depoyaEkle(myMaterialHolderrr, 1);

    }
    public void depoyaEkle(myMaterialHolder myMaterialHolderrr, int multiplier)
    {
        //Debug.Log("depoya ekle  " + myMaterialHolderrr.myMateriall);

        //depoda önceden varsa
        foreach (myMaterialHolder sahip in sahiplerim)
        {
            if (sahip != null)
            {
                if (sahip.myMateriall == myMaterialHolderrr.myMateriall)
                {
                    sahip.amountt += myMaterialHolderrr.amountt * multiplier;
                    depoYaziGuncelle();
                    return;
                }
            }

        }
        //Debug.Log("depoda yeni malzeme ekleniyo");
        //depoda önceden yokmuþ demek ki
        for (int i = 0; i < sahiplerim.Length; i++)
        {
            if (sahiplerim[i].myMateriall==null)
            {

                sahiplerim[i] = new myMaterialHolder(myMaterialHolderrr.myMateriall, myMaterialHolderrr.amountt * multiplier);
                depoYaziGuncelle();
                return;
            }
        }

        depoYaziGuncelle();

    }
    public void depoYaziGuncelle()
    {
        string depodakiler = "";
        foreach(myMaterialHolder mat in sahiplerim)
        {
            if (mat.myMateriall != null)
            {
                depodakiler += mat.amountt +"x \t"+ mat.myMateriall.name + "\n";
            }
        }
        depodakilerim.text = depodakiler;
    }
}
