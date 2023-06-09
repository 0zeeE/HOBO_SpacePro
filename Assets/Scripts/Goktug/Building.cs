using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using UnityEditor;

[System.Serializable]
public class Building : MonoBehaviour
{

    [Header("�retim K�sm�")]
    [SerializeField] public myMaterialHolder[] consume;
    [SerializeField] public myMaterialHolder[] produce;
    [SerializeField] private GameObject uretemiyorum;

    [Header("lvl")]
    [SerializeField] public int myLvl;
    [SerializeField] public bool myLvlIsFull;
    [SerializeField] public int gerekliExp, gerekliExpNow;
    [SerializeField] public int Exp;
    [SerializeField] public GameObject[] eachLvl;
    [SerializeField] public myMaterialHolder[] lvlUpRequ;



    [Header("se�ilen gosterme")]
    [SerializeField] private GameObject chosenSym;
    [SerializeField] public bool amIChosen;
    [SerializeField] public static bool AnyChosen;


    [Header("CD")]
    [SerializeField] public float produceTimeMax;
    [SerializeField] private float produceTime;


    public void setMyLvl(int nowMyLvl)
    {
        foreach(GameObject a in eachLvl)
        {
            a.SetActive(false);
        }
        eachLvl[nowMyLvl].SetActive(true);
    }
    public void loadMe()
    {
        string dataPath = Application.dataPath;
        string filePath = Path.Combine(dataPath, "save01.xml");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);


        XmlNodeList buildingNodes = xmlDoc.SelectNodes("//building_name");
        Building bui = this.gameObject.GetComponent<Building>();
        foreach (XmlNode buildingNode in buildingNodes)
        {
            if (buildingNode.InnerText == bui.gameObject.name)
            {
                XmlNode parentNode = buildingNode.ParentNode;
                int expp = Int32.Parse(parentNode.SelectSingleNode("building_exp").InnerText);
                int Lvll = Int32.Parse(parentNode.SelectSingleNode("building_lvl").InnerText);

                bui.loadMe(Lvll, expp);
                return;
            }
        }

    }
    public void loadMe(int lvl,int exp)
    {
        myLvl = lvl;
        Exp = exp;
        gerekliExpNow = gerekliExp * (myLvl + 1);
        if (gerekliExpNow <= Exp)
        {
            myLvlIsFull = true;
            Exp = gerekliExpNow;

        }
        else
        {
            myLvlIsFull = false;
        }

        setMyLvl(myLvl);
    }
    public void uretHerSeyi()
    {
        Inventory[] Depomuz = GameObject.FindObjectsOfType<Inventory>();
        foreach (myMaterialHolder myMaterialHolderr in consume)
        {
            if (!Depomuz[0].depodaVarMi(myMaterialHolderr))
            {
                Exp--;
                if (Exp <= 0)
                {
                    uretemiyorum.SetActive(true);
                    Exp = 0;
                }
                return;
            }
        }
        foreach (myMaterialHolder myMaterialHolderr in consume)
        {
            Depomuz[0].depodanCikar(myMaterialHolderr);

        }

        foreach (myMaterialHolder myMaterialHolderr in produce)
        {
            Depomuz[0].depoyaEkle(myMaterialHolderr,myLvl);
        }
        uretemiyorum.SetActive(false);
        Exp++;
        if (gerekliExpNow <= Exp)
        {
            myLvlIsFull = true;
            Exp = gerekliExpNow;
        }
        else
        {
            myLvlIsFull = false;
        }
    }
    public void lvlUp()
    {
        if (myLvlIsFull)
        {
            Inventory[] Depomuz = GameObject.FindObjectsOfType<Inventory>();
            foreach (myMaterialHolder myMaterialHolderr in lvlUpRequ)
            {
                if (!Depomuz[0].depodaVarMi(myMaterialHolderr))
                {
                    Debug.Log("cant lvl up");
                    return;
                }
            }
            foreach (myMaterialHolder myMaterialHolderr in lvlUpRequ)
            {
                myMaterialHolder myMaterialHolderWithLvl = new myMaterialHolder(myMaterialHolderr.myMateriall, (myMaterialHolderr.amountt * myLvl));
                Depomuz[0].depodanCikar(myMaterialHolderr);

            }

            myLvlIsFull = false;
            myLvl++;
            Exp = 0;
            gerekliExpNow = gerekliExp * (myLvl + 1);

            setMyLvl(myLvl);//fotonun d�zelmesi i�in

        }

    }
    void Start()
    {
        gerekliExpNow = gerekliExp * (myLvl + 1);
        setClickBisi();
        setMyLvl(myLvl);
    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //Debug.Log("Clicked on " + gameObject.name);
                amIChosen = true;
                AnyChosen = true;
                chosenSym.SetActive(amIChosen);
            }
            else if (hit.collider != null)
            {
                try
                {

                    Building myBuilding = hit.collider.gameObject.GetComponent<Building>();
                    if (myBuilding != null)
                    {
                        amIChosen = false;
                        AnyChosen = true;
                        chosenSym.SetActive(amIChosen);
                    }
                    else
                    {
                        amIChosen = false;
                        AnyChosen = false;
                        chosenSym.SetActive(amIChosen);
                    }
                }
                finally
                {

                }


            }
            /*
            else if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                //Debug.Log(hit.collider.gameObject.name + " was clicked instead of " + gameObject.name);
                amIChosen = false;

                chosenSym.SetActive(amIChosen);
            }*/
           else if (hit.collider == null)
            {
                amIChosen = false;
                AnyChosen = false;
                chosenSym.SetActive(amIChosen);
            }
            
        }

        produceTime -= Time.deltaTime;
        if (produceTime <= 0)
        {
            uretHerSeyi();
            produceTime = produceTimeMax;
        }
    }
    void setClickBisi()
    {

        chosenSym.SetActive(amIChosen);
    }

    public GameObject benSeciliMiyim()
    {
        if (amIChosen == true)
        {
            return this.gameObject;
        }
        return null;
    }
}
