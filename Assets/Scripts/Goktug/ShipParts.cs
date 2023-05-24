using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipParts : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("rokete gidecekler")]
    [SerializeField] private int rocketLevel;
    [SerializeField] private int boosterLevel;
    [SerializeField] private int fuelLevel;

    [Header("seçilen gosterme")]
    [SerializeField] private GameObject chosenSym;
    [SerializeField] public bool amIChosen;
    [SerializeField] public static bool AnyChosen;

    void Start()
    {
        setClickBisi();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Clicked on " + gameObject.name);
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
    void setClickBisi()
    {

        chosenSym.SetActive(amIChosen);
    }
}
