using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTimer: MonoBehaviour
{

    float chargeTime;
    float currentCharge;
    private string key;
    private bool ready;

    public ChargeTimer(float cT, float cC, string key2)
    {
        chargeTime=cT;
        currentCharge=cC;
        key=key2;
        ready=false;
        Debug.Log("started");
    }

    private void Wake()
    {
        //Debug.Log("started");
    }

    public bool IsReady()
    {
        return ready;
    }
  

    // Update is called once per frame
    void Update()
    {
        Debug.Log('h');


    }
}
