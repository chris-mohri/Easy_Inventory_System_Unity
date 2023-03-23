
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CinemachineFreeLook))] 
public class mouseWheel : MonoBehaviour
{
 
    public PlayerControls controls;
    //[SerializeField] private InputActionAsset inputProvider;
    [SerializeField] private CinemachineFreeLook freeLookCameraToZoom;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomAcceleration = 2.5f;
    [SerializeField] private float zoomInnerRange=3;
    [SerializeField] private float zoomOuterRange=15f;
    [SerializeField] private float heightMin;
    [SerializeField] private float heightMax;

    public float addRadius=3f;
    private float currentMiddleRigRadius;
    private float newMiddleRigRadius; 
    public float subtractHeight=5f;
    private float currentMiddleRigHeight;
    private float newMiddleRigHeight;
    private float ogRad, ogHeight; 

    [SerializeField] private float zoomYAxis=0f;

    public float ZoomYAxis{
        get {return zoomYAxis;}
        set{
            if (zoomYAxis==value) {return;}
            zoomYAxis=value;
            AdjustCameraZoomIndex(zoomYAxis);
        } 
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Camera.MouseZoom.performed+=cntxt=> ZoomYAxis=cntxt.ReadValue<float>();
        controls.Camera.MouseZoom.canceled+=cntxt=> ZoomYAxis=0f;
        controls.Camera.ScrollPress.performed+=cntxt=> resetCam();


        currentMiddleRigRadius=freeLookCameraToZoom.m_Orbits[1].m_Radius;
        newMiddleRigRadius=currentMiddleRigRadius+.01f;

        currentMiddleRigHeight=freeLookCameraToZoom.m_Orbits[1].m_Height;
        newMiddleRigHeight=currentMiddleRigHeight+.01f;

        ogRad=currentMiddleRigRadius;
        ogHeight=currentMiddleRigHeight;

        heightMin=currentMiddleRigHeight-subtractHeight;
        heightMax=currentMiddleRigHeight+subtractHeight;
        

        //resetCam();


    }
    private void resetCam()        //SOMEHOW GET THIS TO WORK !!!!!
    {
        //Debug.Log("currentMiddleRigHeight");
        newMiddleRigHeight=ogHeight;
        newMiddleRigRadius=ogRad;
    }

    private void OnEnable()
    {
        controls.Camera.Enable();
    }

    private void OnDisable()
    {
        controls.Camera.Disable();
    }

    private void LateUpdate()
    {
        updateZoomLevel();
    }

    private void updateZoomLevel()
    {
        //Debug.Log(zoomYAxis);
        if (currentMiddleRigRadius == newMiddleRigRadius){return;}
         currentMiddleRigRadius = Mathf.Lerp(currentMiddleRigRadius, newMiddleRigRadius, zoomAcceleration*Time.deltaTime);
         currentMiddleRigRadius = Mathf.Clamp(currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);
 
         currentMiddleRigHeight = Mathf.Lerp(currentMiddleRigHeight, newMiddleRigHeight, zoomAcceleration*Time.deltaTime/2);
         currentMiddleRigHeight = Mathf.Clamp(currentMiddleRigHeight, heightMin, heightMax);
         currentMiddleRigHeight = Mathf.Clamp(currentMiddleRigHeight, 0, heightMax);
         
         
         //freeLookCameraToZoom.m_Orbits[0].m_Radius = currentMiddleRigRadius;
         freeLookCameraToZoom.m_Orbits[1].m_Radius = currentMiddleRigRadius;
         //freeLookCameraToZoom.m_Orbits[2].m_Radius = currentMiddleRigRadius;

         //freeLookCameraToZoom.m_Orbits[0].m_Height = currentMiddleRigHeight;
         freeLookCameraToZoom.m_Orbits[1].m_Height = currentMiddleRigHeight;
         //freeLookCameraToZoom.m_Orbits[2].m_Height = currentMiddleRigHeight;
         
    }

    public void AdjustCameraZoomIndex(float zoomYAxis){
        //Debug.Log('a');
        
        if (zoomYAxis==0){return;}
        if (zoomYAxis<0)
        {
            newMiddleRigRadius = currentMiddleRigRadius+zoomSpeed;
            newMiddleRigHeight = currentMiddleRigHeight+zoomSpeed;
        }
        if (zoomYAxis>0)
        {
            newMiddleRigRadius = currentMiddleRigRadius-zoomSpeed;
            newMiddleRigHeight = currentMiddleRigHeight-zoomSpeed;
        }
    }


    void Update()
    {
        //Debug.Log('y');
    }
}
