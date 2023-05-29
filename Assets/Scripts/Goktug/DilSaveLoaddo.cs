using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DilSaveLoaddo : MonoBehaviour
{
    // Start is called before the first frame update


    // Combine the data path with the desired file name
    string filePathh;

    void Start()
    {
        // Get the path to the game's data folder
        string dataPath = Application.dataPath;
        string filePath = Path.Combine(dataPath, "Buildings.xml");
        filePathh = filePath;
        // Check if the file already exists
        if (File.Exists(filePath))
        {

            File.AppendAllText(filePath, ("<!--"+ "BURAYA BAKARLAR" +"-->"));

            Debug.Log("Text added to the file at: " + filePath);
        }
        else
        {
            // Create a new file and write text
            File.WriteAllText(filePath, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            Debug.Log("Text file created at: " + filePath);
        }
    }
    public void saveGame()
    {
        if (File.Exists(filePathh))
        {

            try
            {
                File.Delete(filePathh);
                Debug.Log("File deleted successfully.");
            }
            catch (IOException e)
            {
                Debug.Log("An error occurred while deleting the file: " + e.Message);
            }
            saveGame();
        }
        else
        {
            // Create a new file and write text
            File.WriteAllText(filePathh, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            Debug.Log("Text file created at: " + filePathh);
        }

        File.AppendAllText(filePathh, "<All>");

        Inventory[] Depomuz = GameObject.FindObjectsOfType<Inventory>();
        foreach(Inventory depo in Depomuz)
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

        Building[] builds = GameObject.FindObjectsOfType<Building>();
        foreach (Building bui in builds)
        {
            if (bui != null)
            {
                File.AppendAllText(filePathh, "<building>");
                File.AppendAllText(filePathh, ("<building_name>" + bui.gameObject.name + "</building_name>"));
                File.AppendAllText(filePathh, ("<building_exp>" + bui.Exp + "</building_exp>"));

                File.AppendAllText(filePathh, "</building>");
            }
        }

        File.AppendAllText(filePathh, "</All>");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            saveGame();
        }
    }
}
