
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelMeter : MonoBehaviour
{
    [SerializeField] private GameObject rocket;


    private const float MAX_SPEED_ANGLE = 90;
    private const float ZERO_SPEED_ANGLE = 180;

    private Transform needleTranform;
    private Transform speedLabelTemplateTransform;

    private float maxFuel;
    private float fuelAmount;

    private float fuel;
    
    

    private void Awake()
    {
        needleTranform = transform.Find("needle");
        speedLabelTemplateTransform = transform.Find("speedLabelTemplate");
        speedLabelTemplateTransform.gameObject.SetActive(false);

        SetFuel();
        



        fuelAmount = 0f;
        maxFuel = 100f;

        CreateFuelLabels();
    }

    private void Update()
    {
        SetFuel();

        HandleObjectFuel();

        

        needleTranform.eulerAngles = new Vector3(0, 0, GetFuelRotation());
    }

    private void HandleObjectFuel()
    {

        fuelAmount = fuel;


        fuelAmount = Mathf.Clamp(fuelAmount, 0f, maxFuel);
    }

    private void CreateFuelLabels()
    {
        int labelAmount = 4;
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        for (int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = ZERO_SPEED_ANGLE - labelSpeedNormalized * totalAngleSize;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNormalized * maxFuel).ToString();
            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }

        needleTranform.SetAsLastSibling();
    }

    private float GetFuelRotation()
    {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNormalized = fuelAmount / maxFuel;

        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }

    private void SetFuel()
    {
        fuel = rocket.GetComponent<RocketMovement>().GetFuel();
    }

    


}
