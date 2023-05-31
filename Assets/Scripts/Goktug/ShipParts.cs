using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipParts : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Partýn tanýmý")]
    [SerializeField] public string myAd;
    [SerializeField] public string myDes;
    [SerializeField] private benNeyim benBuyum;//CLASS ile yapmam daha doðru ama caným böyle istedi basit kod diye
    //Doðrusunda ben neyimi ve seviyeyi barýndýran class lazým
    public enum benNeyim
    {
        rocketLevel,
        boosterLevel,
        fuelLevel
    };
    [Header("seçilen gosterme")]
    [SerializeField] private GameObject chosenSym;
    [SerializeField] public bool amIChosen;
    [SerializeField] public static bool AnyChosen;

    [Header("lvl up")]
    [SerializeField] public int myLvl;
    [SerializeField] private int myLvlmax;
    [SerializeField] public GameObject[] eachLvl;
    [SerializeField] public myMaterialHolder[] lvlUpRequ;
    void Start()
    {
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

                    ShipParts myShipParts = hit.collider.gameObject.GetComponent<ShipParts>();
                    if (myShipParts != null)
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
            else if (hit.collider == null)
            {
                amIChosen = false;
                AnyChosen = false;
                chosenSym.SetActive(amIChosen);
            }

        }



    }

    public void loadMe(int lvl)
    {
        myLvl = lvl;
        setMyLvl(myLvl);
    }
    public void lvlUp()
    {
        if (myLvlmax > myLvl)
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
                if (myLvl == 0)
                {
                    myMaterialHolder myMaterialHolderWithLvl = new myMaterialHolder(myMaterialHolderr.myMateriall, (myMaterialHolderr.amountt * 1));
                }
                else
                {
                    myMaterialHolder myMaterialHolderWithLvl = new myMaterialHolder(myMaterialHolderr.myMateriall, (myMaterialHolderr.amountt * myLvl));
                }

                Depomuz[0].depodanCikar(myMaterialHolderr);

            }

            myLvl++;
            setMyLvl(myLvl);//fotonun düzelmesi için

        }
        else
        {
            Debug.Log("max lvl");
        }
        


    }
    public void setMyLvl(int nowMyLvl)
    {
        foreach (GameObject a in eachLvl)
        {
            a.SetActive(false);
        }
        eachLvl[nowMyLvl].SetActive(true);
        if(benBuyum== ShipParts.benNeyim.boosterLevel)
        {
            ShipParts4DataHolder.boosterLevel = myLvl;
        }
        else if (benBuyum == ShipParts.benNeyim.rocketLevel)
        {
            ShipParts4DataHolder.rocketLevel = myLvl;
        }
        else if (benBuyum == ShipParts.benNeyim.fuelLevel)
        {
            ShipParts4DataHolder.fuelLevel = myLvl;
        }
    }

    void setClickBisi()
    {

        chosenSym.SetActive(amIChosen);
    }
}
