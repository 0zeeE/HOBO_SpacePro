using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;

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
    public void saveGame()
    {
        if (File.Exists(filePathh))
        {

            try
            {
                File.Delete(filePathh);
                Debug.Log("File deleted successfully.");
                //saveGame();
            }
            catch (IOException e)
            {
                Debug.Log("An error occurred while deleting the file: " + e.Message);
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

        ShipParts[] shipParts = GameObject.FindObjectsOfType<ShipParts>();
        File.AppendAllText(filePathh, "<rootShip>");
        foreach (ShipParts shipPart in shipParts)
        {
            if (shipPart != null)
            {
                File.AppendAllText(filePathh, "<ship>");
                File.AppendAllText(filePathh, ("<ship_part>" + shipPart.gameObject.name + "</ship_part>"));
                File.AppendAllText(filePathh, ("<ship_partLvl>" + shipPart.myLvl+ "</ship_part>"));
                File.AppendAllText(filePathh, "</ship>");
            }
        }
        File.AppendAllText(filePathh, "</rootShip>");

        File.AppendAllText(filePathh, "</All>");
    }
    public void LoadGame()
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
        /*
        foreach (XmlNode buildingNode in buildingNodes)
        {
            Student student = new Student();

            XmlNode nameNode = studentNode.SelectSingleNode("name");
            student.Name = nameNode.InnerText;

            XmlNode numberNode = studentNode.SelectSingleNode("number");
            student.Number = numberNode.InnerText;

            Console.WriteLine("Name: " + student.Name);
            Console.WriteLine("Number: " + student.Number);
            Console.WriteLine();
        }
        */
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            saveGame();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadGame();
        }
    }
}
