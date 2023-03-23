using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{

    public float addHeight=5f;
    public float speed=0.5f;
    public float toDiameter;
    public float toHeight;
    public bool start=true;
    public bool end=false;
    private Vector3 toVector;
    private float timer=0f;
    

    private Vector3 ogVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        ogVector=transform.localScale;
        toVector=new Vector3(0f,0f,0f);
        transform.localScale=toVector;

        Vector3 temp=new Vector3(Variables.playerPos.x, Variables.playerPos.y+addHeight, Variables.playerPos.z);
        transform.position = temp;

        if (Variables.worldPos!=new Vector3(0f,0f,0f)){
            transform.LookAt(Variables.worldPos);
            transform.Rotate(90f,0f, 0f);
        }
    }



    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer>=2.5f)
        {
            start=false;
            end=true;
        }
        //float temp = toVector.x+toDiameter/speed*Time.deltaTime;
        if (start==true)
        {
            //Debug.Log(speed);
            toVector.x=Mathf.Clamp(toVector.x+toDiameter/speed*Time.deltaTime, 0f, toDiameter);
            toVector.y=Mathf.Clamp(toVector.y+toHeight/speed*Time.deltaTime, 0f, toHeight);
            toVector.z=Mathf.Clamp(toVector.z+toDiameter/speed*Time.deltaTime, 0f, toDiameter);
        
        }

        if (end==true)
        {
            toVector.x=Mathf.Clamp(toVector.x-toDiameter/speed*Time.deltaTime, 0f, toDiameter);
            toVector.y=Mathf.Clamp(toVector.y-toHeight/speed*Time.deltaTime, 0f, toHeight);
            toVector.z=Mathf.Clamp(toVector.z-toDiameter/speed*Time.deltaTime, 0f, toDiameter);
            if (toVector.x<=0)
            {
                Destroy(gameObject);
            }
        }

        //Mathf.clamp()

        transform.localScale=toVector;

        //follows character
        //Vector3 temp=new Vector3(Variables.playerPos.x, Variables.playerPos.y+addHeight, Variables.playerPos.z);
        //transform.position = temp;
        
        


        //Debug.Log(Variables.worldPos);
        //transform.Rotate(90f,0f, 0f);
    }
}
