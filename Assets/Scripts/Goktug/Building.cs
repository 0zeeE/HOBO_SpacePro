using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : MonoBehaviour
{

    [Header("Üretim Kýsmý")]
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



    [Header("seçilen gosterme")]
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

            setMyLvl(myLvl);//fotonun düzelmesi için

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
