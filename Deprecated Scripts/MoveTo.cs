using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        //GameObject GO = GameObject.Find("Global Variables");
        //GlobalVariables vars =GO.GetComponent();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 worldPos = vars.worldPos;
        transform.position = Variables.worldPos;
    }
}
