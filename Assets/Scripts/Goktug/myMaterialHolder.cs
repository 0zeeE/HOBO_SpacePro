using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class myMaterialHolder
{
    [SerializeField] public myMaterial myMateriall;
    [SerializeField] public float amountt;


    public myMaterialHolder(myMaterial myMateriall_, float amount)
    {
        amountt = amount;
        myMateriall = myMateriall_;
    }


}
