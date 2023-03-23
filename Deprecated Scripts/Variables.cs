using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private Camera cam;
    public PlayerControls controls;
    ThirdPersonMovementScript tpms;
    int layerMask;


    public static Vector3 playerPos;
    public static Vector3 worldPos;
    

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
    
    void Awake()
    {
        controls = new PlayerControls();
        //ThirdPersonMovementScript tpms = GetComponent
        tpms= GetComponent<ThirdPersonMovementScript>();
        cam=Camera.main;

        layerMask = 1 << 7;
        layerMask = ~layerMask;
    }

    private void OnEnable()
    {
        controls.Mouse.Enable();
    }
    private void OnDisable()
    {
        controls.Mouse.Disable();
    }

    // Update is called once per frame

    /*
    void OnGUI()
    {
        Vector2 temp = controls.Mouse.Position.ReadValue<Vector2>();


        worldPos = cam.ScreenToWorldPoint(new Vector3(temp.x, temp.y, 10f));
        Debug.Log(worldPos);
    }

    */

 /*
    void Update()
    {
        
    Plane plane = new Plane(Vector3.up, 0);

    float distance;
    Ray ray = cam.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());

    if (plane.Raycast(ray, out distance))
    {
        worldPos = ray.GetPoint(distance);
    }


    transform.position = worldPos;
    }
*/
    public static void SetPos(Vector3 pos)
    {
        playerPos=pos;
    }

    void Update()
    {
        
    //Plane plane = new Plane(Vector3.up, 0);

    //float distance;
    Ray ray = cam.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
    if (Physics.Raycast(ray, out RaycastHit raycastHit, 50f, layerMask))
    {
        worldPos = raycastHit.point;
    }
    else
    {
        worldPos=new Vector3();
    }


    //transform.position = worldPos;
    }
    

}
