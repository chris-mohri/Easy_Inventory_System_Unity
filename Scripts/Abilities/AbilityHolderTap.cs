using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolderTap : MonoBehaviour
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
        controls.Player.Skill.performed +=cntxt => startCounter(cntxt);

    }

    private void startCounter(InputAction.CallbackContext context)
    {
        wasStarted=true;
 
        
        
    }


    void OnEnable()
    {
        controls.Player.Skill.Enable();
    }
    void OnDisable()
    {
        controls.Player.Skill.Disable();
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

                {
                    if (wasStarted){

                    activate=true;
                    state=AbilityState.ready;
                    wasStarted=false;

                    }
                    
             
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
