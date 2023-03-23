using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability: ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float chargeTime;
    public float doubleTapLimit=0.2f;
    public float pressCount;


    public virtual void Activate(GameObject parent){}
    //public bool ready=true;

    
}
