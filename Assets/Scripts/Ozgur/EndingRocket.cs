using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingRocket : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject rocketSprite;
    private Transform rocketTransform1;
    private Transform rocketTransform2;
    public float rotationSpeed = 150f;
    public float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rocketTransform1 = rocket.GetComponent<Transform>();
        rocketTransform2 = rocketSprite.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        rocketTransform1.Translate(0, moveSpeed * Time.deltaTime, 0);
    }

    void Rotate()
    {
        rocketTransform2.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
