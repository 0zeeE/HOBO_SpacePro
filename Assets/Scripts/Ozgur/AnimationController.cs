using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animatedObject;
    


    public void setAnimatedObject(bool setvalue)
    {
        animatedObject.SetBool("isPushing", setvalue);
    }
}
