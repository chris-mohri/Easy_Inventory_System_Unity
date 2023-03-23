using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate (GameObject parent)
    {
        TopDownCharacterController tdcc = parent.GetComponent<TopDownCharacterController>();
        //tdcc.sprinting=true;
        tdcc.addForce(tdcc.currentDirection * dashVelocity);
        //Debug.Log("Hell yeah");
        //a

    }
}
