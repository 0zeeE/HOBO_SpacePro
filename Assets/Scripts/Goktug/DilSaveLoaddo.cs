using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DilSaveLoaddo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the path to the game's data folder
        string dataPath = Application.dataPath;

        // Combine the data path with the desired file name
        string filePath = Path.Combine(dataPath, "Buildings.xml");

        // Check if the file already exists
        if (File.Exists(filePath))
        {
            // Add text to the existing file
            File.AppendAllText(filePath, "New line added!");

            Debug.Log("Text added to the file at: " + filePath);
        }
        else
        {
            // Create a new file and write text
            File.WriteAllText(filePath, "Hello, world!");

            Debug.Log("Text file created at: " + filePath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
