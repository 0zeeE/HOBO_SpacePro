using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : MonoBehaviour
{

    [Header("Üretim Kýsmý")]
    [SerializeField] public myMaterialHolder[] consume;
    [SerializeField] public myMaterialHolder[] produce;

    [Header("lvl")]
    [SerializeField] public int myLvl;
    [SerializeField] public GameObject[] eachLvl;


    [Header("seçilen gosterme")]
    [SerializeField] private GameObject chosenSym;
    [SerializeField] private bool amIChosen;
    [SerializeField] public static bool AnyChosen;


    [Header("CD")]
    [SerializeField] private float produceTimeMax;
    private float produceTime;

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

    }
    void Start()
    {
        setClickBisi();
        setMyLvl(myLvl);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setMyLvl(myLvl);
        }



        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                //Debug.Log(hit.collider.gameObject.name + " was clicked instead of " + gameObject.name);
                amIChosen = false;

                chosenSym.SetActive(amIChosen);
            }
            else if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Clicked on " + gameObject.name);
                amIChosen = true;
                AnyChosen = true;
                chosenSym.SetActive(amIChosen);
            }else if (hit.collider == null)
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
