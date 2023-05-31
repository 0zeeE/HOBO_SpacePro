using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silBeni : MonoBehaviour
{

    float cd = 2;
    void Update()
    {
        cd -= Time.deltaTime;
        if (cd <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
