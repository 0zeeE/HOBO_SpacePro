using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using UnityEditor;

public class DilSaveLoaddo : MonoBehaviour
{
    // Start is called before the first frame update


    // Combine the data path with the desired file name
    string filePathh;

    void Start()
    {
        // Get the path to the game's data folder
        string dataPath = Application.dataPath;
        string filePath = Path.Combine(dataPath, "save01.xml");
        filePathh = filePath;


    }
    public void saveInv()
    {
        Inventory[] Depomuz = GameObject.FindObjectsOfType<Inventory>();
        File.AppendAllText(filePathh, "<inv>");
        foreach (Inventory depo in Depomuz)
        {
            if (depo != null)
            {
                File.AppendAllText(filePathh, "<depo>");
                foreach (myMaterialHolder myMatHolder in depo.sahiplerim)
                {
                    if (myMatHolder.myMateriall != null)
                    {
                        File.AppendAllText(filePathh, "<item>");

                        File.AppendAllText(filePathh, "<matName>");
                        File.AppendAllText(filePathh, myMatHolder.myMateriall.name);
                        File.AppendAllText(filePathh, "</matName>");
                        File.AppendAllText(filePathh, "<amountt>");
                        File.AppendAllText(filePathh, myMatHolder.amountt.ToString());
                        File.AppendAllText(filePathh, "</amountt>");

                        File.AppendAllText(filePathh, "</item>");
                    }

                }
                File.AppendAllText(filePathh, "</depo>");
            }

        }
        File.AppendAllText(filePathh, "</inv>");
    }
    public void saveBuildings()
    {
        Building[] builds = GameObject.FindObjectsOfType<Building>();
        File.AppendAllText(filePathh, "<tiles>");
        foreach (Building bui in builds)
        {
            if (bui != null)
            {
                File.AppendAllText(filePathh, "<building>");
                File.AppendAllText(filePathh, ("<building_name>" + bui.gameObject.name + "</building_name>"));
                File.AppendAllText(filePathh, ("<building_exp>" + bui.Exp + "</building_exp>"));
                File.AppendAllText(filePathh, ("<building_lvl>" + bui.myLvl + "</building_lvl>"));
                File.AppendAllText(filePathh, "</building>");
            }
        }
        File.AppendAllText(filePathh, "</tiles>");
    }
    public void saveShip()
    {
        ShipParts[] shipParts = GameObject.FindObjectsOfType<ShipParts>();
        File.AppendAllText(filePathh, "<rootShip>");
        foreach (ShipParts shipPart in shipParts)
        {
            if (shipPart != null)
            {
                File.AppendAllText(filePathh, "<ship>");
                File.AppendAllText(filePathh, ("<ship_part>" + shipPart.gameObject.name + "</ship_part>"));
                File.AppendAllText(filePathh, ("<ship_partLvl>" + shipPart.myLvl + "</ship_partLvl>"));
                File.AppendAllText(filePathh, "</ship>");
            }
        }
        File.AppendAllText(filePathh, "</rootShip>");
    }
    public void saveGame()
    {
        if (File.Exists(filePathh))
        {

            try
            {
                File.Delete(filePathh);
                Debug.Log("File overwrited successfully.");
                //saveGame();
            }
            catch (IOException e)
            {
                Debug.Log("An error occurred while overwriting the file: " + e.Message);
            }
            
        }
        else
        {
            // Create a new file and write text
            //File.WriteAllText(filePathh, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            Debug.Log("Text file created at: " + filePathh);
        }

        File.WriteAllText(filePathh, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        File.AppendAllText(filePathh, "<All>");



        saveInv();
        saveBuildings();
        saveShip();


        File.AppendAllText(filePathh, "</All>");
    }


    
    private myMaterialHolder findMyMaterialHolder(string nameOfObj)
    {
        myMaterial MyMaterial = (myMaterial)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Scriptables/"+ nameOfObj+ ".asset"), typeof(myMaterial));

        myMaterialHolder newMatHolder = new myMaterialHolder(MyMaterial, 1);
        return newMatHolder;
    }
    private void LoadShip()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePathh);

        ShipParts[] ships = GameObject.FindObjectsOfType<ShipParts>();
        XmlNodeList shipNodes = xmlDoc.SelectNodes("//ship_part");
        foreach (ShipParts shi in ships)
        {
            if (shi != null)
            {
                foreach (XmlNode shipNode in shipNodes)
                {
                    if (shipNode.InnerText == shi.gameObject.name)
                    {
                        XmlNode parentNode = shipNode.ParentNode;
                        int Lvll = Int32.Parse(parentNode.SelectSingleNode("ship_partLvl").InnerText);
                        Console.WriteLine("ship: " + shipNode.InnerText);
                        Console.WriteLine("Level: " + Lvll);
                        shi.loadMe(Lvll);
                    }
                }
            }
        }
    }
    private void LoadBuildings()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePathh);

        Building[] builds = GameObject.FindObjectsOfType<Building>();
        XmlNodeList buildingNodes = xmlDoc.SelectNodes("//building_name");
        foreach (Building bui in builds)
        {
            if (bui != null)
            {
                foreach (XmlNode buildingNode in buildingNodes)
                {
                    if (buildingNode.InnerText == bui.gameObject.name)
                    {
                        XmlNode parentNode = buildingNode.ParentNode;
                        int expp = Int32.Parse(parentNode.SelectSingleNode("building_exp").InnerText);
                        int Lvll = Int32.Parse(parentNode.SelectSingleNode("building_lvl").InnerText);
                        Console.WriteLine("building: " + buildingNode.InnerText);
                        Console.WriteLine("Level: " + Lvll);
                        bui.loadMe(Lvll, expp);
                    }
                }
            }
        }
    }
    private void LoadInv()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePathh);

        Inventory[] invs = GameObject.FindObjectsOfType<Inventory>();
        XmlNodeList invNodes = xmlDoc.SelectNodes("//matName");

        foreach (XmlNode invNode in invNodes)
        {
            /*
            Debug.LogWarning(invNode.InnerText);//Bor
            Debug.LogError(invNode.ParentNode.InnerText);//Bor16
            Debug.Log(invNode.ParentNode.SelectSingleNode("matName").InnerText);//Bor
            */

            string namee = invNode.ParentNode.SelectSingleNode("matName").InnerText;
            int adet = Int32.Parse(invNode.ParentNode.SelectSingleNode("amountt").InnerText);
            //Debug.Log("namee: " + namee);
            //Debug.Log("adet: " + adet);

            invs[0].depoyaEkle(findMyMaterialHolder(namee), adet);
        }

    }
    public void LoadGame()
    {


        LoadInv();
        LoadBuildings();
        LoadShip();


        StartCoroutine(loadEkraniSil());



    }
    IEnumerator loadEkraniSil()
    {
        yield return new WaitForSeconds(2);
        try
        {
            Destroy(GameObject.Find("loadSc"));
        }
        catch
        {

        }
    }
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.U))
        {
            saveGame();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadGame();
        }
        */
    }
}
