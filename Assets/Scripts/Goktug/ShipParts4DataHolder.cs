using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipParts4DataHolder : MonoBehaviour
{
    [Header("rokete gidecekler")]
    [SerializeField] public static int rocketLevel;
    [SerializeField] public static int boosterLevel;
    [SerializeField] public static int fuelLevel;
    // Start is called before the first frame update
    public static ShipParts4DataHolder Instance;
    void Start()
    {
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

        //Debug.Log("rocketLevel: " + rocketLevel);
        //Debug.Log("boosterLevel: " + boosterLevel);
        //Debug.Log("fuelLevel: " + fuelLevel);
    }
}
