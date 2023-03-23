using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolderDoubleTap : MonoBehaviour
{

    public PlayerControls controls;

    public Ability ability;
    float cooldownTime;
    //private float cooldownTime;
    float activeTime;

    float chargeTime;
    //public float currentChargeTime;

    float doubleTapLimit;
    //private float currentDoubleTapLimit;
    private float pressCount;


    //private float dashType;
    private bool wasStarted=false;
    private bool activate=false;


    //private ChargeTimer chargeTimer;
    
    enum AbilityState{
        ready,
        charging,
        active,
        cooldown
    }

    AbilityState state = AbilityState.charging;

    public void Awake()
    {
        cooldownTime=ability.cooldownTime;
        chargeTime=ability.chargeTime;
        doubleTapLimit=ability.doubleTapLimit;
        activeTime=ability.activeTime;
        pressCount=ability.pressCount;
        //activeTime=
        controls = new PlayerControls();
        controls.Player.Dash.performed +=cntxt => startCounter(cntxt);

    }

    private void startCounter(InputAction.CallbackContext context)
    {
        wasStarted=true;
        if (state==AbilityState.charging &&(context.ReadValue<float>()==1f || context.ReadValue<float>()==-1f))
        {
            if (pressCount==ability.pressCount) //if dash was jsut started (tapped once)
            {
                doubleTapLimit=ability.doubleTapLimit;
                
                //dashType=context.ReadValue<float>();
            }

            //if starts dash left then right   / vise versa
            //if (dashType!=context.ReadValue<float>())
           // {
            //    pressCount=ability.pressCount;
           //     doubleTapLimit=ability.doubleTapLimit;
                
           //     dashType=context.ReadValue<float>();
            //}

            //Debug.Log(context.ReadValue<float>());

            pressCount-=1;
            
        }
        
    }

    void OnEnable()
    {
        controls.Player.Dash.Enable();
    }
    void OnDisable()
    {
        controls.Player.Dash.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("please print");
        
        switch(state)
        {
            case AbilityState.ready:
                if(activate==true)
                {
                    ability.Activate(gameObject);
                    state=AbilityState.active;
                    //activeTime=ability.activeTime;
                    activate=false;
                }

                break;

       
            case AbilityState.charging:

                
                if (wasStarted)
                    {

                        if (doubleTapLimit>0 && pressCount<=0)
                         {
                             activate=true;
                             pressCount=ability.pressCount;
                             wasStarted=false;
                             //Debug.Log("success");
                             doubleTapLimit=ability.doubleTapLimit;
                             state=AbilityState.ready;
                        }

                         if (doubleTapLimit<=0f)
                        {
                             pressCount=ability.pressCount;
                             wasStarted=false;
                             activate=false;
                             //Debug.Log(currentDashSpaceLimit);
                             //Debug.Log(currentDashSpaceLimit<=0f);
                             doubleTapLimit=ability.doubleTapLimit;
                         }

                         doubleTapLimit-=1f*Time.deltaTime;
             
        }

                break;



            case AbilityState.active:
                activate=false;

                if (activeTime>0){
                    activeTime-=Time.deltaTime;
                }
                else{
                    activeTime=ability.activeTime;
                    state=AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                

                break;




            case AbilityState.cooldown:
                activate=false;

                if (cooldownTime>0){
                    cooldownTime-=Time.deltaTime;
                }
                else{
                    cooldownTime=ability.cooldownTime;
                    state=AbilityState.charging;
                }
                //Debug.Log(cooldownTime);
                break;
            
        }


        //Debug.Log(currentDashSpaceLimit);
        //Debug.Log(controls.Player.Dash.ReadValue<float>());
        //double tap
        
        
        //Debug.Log(wasStarted);



        
    }
}
