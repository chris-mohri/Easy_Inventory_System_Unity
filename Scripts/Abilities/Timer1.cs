using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    private float coolDown;
    private float currentTime;
    private bool ready=true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime=0;
    }

    public bool IsReady()
    {
        return ready;
    }
    public bool Activate()
    {
        if (ready)
        {
            ready=false;
            currentTime=coolDown;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime<=0 && ready==false)
        {
            ready=true;
            currentTime=0;
        }

        else if (currentTime>0){
            currentTime-=1*Time.deltaTime;
        }


    }
}
