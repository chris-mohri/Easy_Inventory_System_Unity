using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RainOfTerror : Ability
{
    //public float dashVelocity;
    public GameObject gate;

    public override void Activate (GameObject parent)
    {
        ThirdPersonMovementScript tpms = parent.GetComponent<ThirdPersonMovementScript>();
        //tpms.sprintContinue=true;
        //tpms.AddImpact(tpms.moveDir, dashVelocity);
        Debug.Log("EA!!");
        //gate.active=true;
        //GameObject GO = Instantiate(gate);
        //Invoke(GO.end=true, 3f);//this will happen after 2 seconds
        
    }
}
