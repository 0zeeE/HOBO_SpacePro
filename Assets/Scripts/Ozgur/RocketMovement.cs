using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int rocketLevel = 1;
    public int fuelLevel = 1;
    [SerializeField]private float flySpeed = 400f;
    [SerializeField]private float rotateSpeed = 4f;
    [SerializeField] private float fuelAmount = 50f;
    [SerializeField] private float fuelDecreaseAmount = 2f;
    public float maxHeight;
    Vector2 move;
    private Transform transform;
    private Rigidbody2D rigidbody;

    [SerializeField] private GameObject soundControl;
    

    void Awake()
    {
        maxHeight = 0;
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        SetRocketLevel();
        SetFuelLevel();
        
    }

    // Update is called once per frame
    void Update()
    {

        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Debug.Log("Yakit miktari: " + fuelAmount);
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && fuelAmount > 0)
        {
            soundControl.SetActive(true);
        }
        else
        {
            soundControl.SetActive(false);
        }

        if (fuelAmount > 0)
        {
            


            if (move.y > 0)
            {
                

                rigidbody.AddForce(this.gameObject.transform.up * flySpeed * Time.deltaTime);
                fuelAmount = fuelAmount - Time.deltaTime * fuelDecreaseAmount;
            }
            if(move.x != 0)
            {
                //rigidbody.AddForce(new Vector2(move.x,0) * rotateSpeed * Time.deltaTime);
                fuelAmount = fuelAmount - Time.deltaTime * fuelDecreaseAmount;
                transform.Rotate(0, 0, -move.x);
            } 
        }

        CalculateMaxHeight();
        
    }

    public void SetRocketLevel()
    {
        if (rocketLevel == 1)
        {
            flySpeed = 600f;
            rotateSpeed = 4f;
        }
        else if (rocketLevel == 2)
        {
            flySpeed = 900f;
            rotateSpeed = 5f;
        }
        else if (rocketLevel == 3)
        {
            flySpeed = 1000f;
            rotateSpeed = 6f;
        }
        else
        {
            Debug.Log("Roket seviyesi belirlenmedi.");
        }
    }

    public void SetFuelLevel()
    {
        if(fuelLevel == 1)
        {
            fuelAmount = 30f;
        }
        else if(fuelLevel == 2)
        {
            fuelAmount = 50f;
        }
        else if (fuelLevel == 3)
        {
            fuelAmount = 100f;
        }
        else
        {
            Debug.Log("Yakit seviyesi ayarlanmadi");
        }
    }

    private void CalculateMaxHeight()
    {
        if(transform.position.y > maxHeight)
        {
            maxHeight = transform.position.y * 5;
        }
        Debug.Log("Max Height: " + maxHeight);
    }

    public float GetFuel()
    {
        return fuelAmount;
    }
}