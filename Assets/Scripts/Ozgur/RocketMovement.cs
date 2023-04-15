using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float flySpeed = 400f;
    public float rotateSpeed = 4f;
    public float fuelAmount = 50f;
    public float fuelDecreaseAmount = 2f;
    public GameObject DataHolder;
    Vector2 move;
    Transform transform;
    public Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        DataHolder = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Debug.Log(fuelAmount);
    }

    private void FixedUpdate()
    {
        if(fuelAmount > 0)
        {
            if(move.y != 0)
            {
                rigidbody.AddForce(move * flySpeed * Time.deltaTime);
                fuelAmount = fuelAmount - Time.deltaTime * fuelDecreaseAmount;
            }
            if(move.x != 0)
            {
                rigidbody.AddForce(move * rotateSpeed * Time.deltaTime);
                fuelAmount = fuelAmount - Time.deltaTime * fuelDecreaseAmount;
                transform.Rotate(0, 0, -move.x);
            }
            
            
            
        }
        
    }
}
