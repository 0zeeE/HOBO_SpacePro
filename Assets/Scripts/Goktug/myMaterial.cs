using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "myMaterial", menuName = "MyObjs/myMaterial")]
[System.Serializable]
public class myMaterial : ScriptableObject
{
    public new string name;
    public string description;


    public Sprite artWork;

}
