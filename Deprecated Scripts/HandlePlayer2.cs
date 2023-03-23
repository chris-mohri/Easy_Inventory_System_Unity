using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayer2 : MonoBehaviour
{
    //InputManager inputManager;
    ThirdPersonMovementScript tpms;
    //Variables vars;
    

    private void Awake()
    {
        //inputManager = GetComponent<InputManager>();
        tpms= GetComponent<ThirdPersonMovementScript>();
        //vars= GetComponent<Variables>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputManager.HandleAllInputs();
        tpms.HandleMovement();
        tpms.HandleRotate();
        //Debug.Log(Variables.worldPos);
        Variables.SetPos(tpms.transform.position); //sends info to global variables
    }
}
