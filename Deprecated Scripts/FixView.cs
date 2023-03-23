using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineFreeLook))] 

public class FixView : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook cam;
    public float xRange = 10f;
    public float yRange= 10f;
    private float currentX;
    private float currentY;
    private float yMin, yMax;
    private float xMin, xMax;

    void Awake(){
        yMin = cam.m_YAxis.Value-yRange;
        yMax = cam.m_YAxis.Value+yRange;

        xMin = cam.m_XAxis.Value-xRange;
        xMax = cam.m_XAxis.Value+xRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //currentY = cam.m_YAxis.Value;
        cam.m_YAxis.m_MaxValue=yMax;
        cam.m_YAxis.m_MinValue=yMin;
        cam.m_XAxis.m_MaxValue=xMax;
        cam.m_XAxis.m_MinValue=xMin;
        //cam.m_XAxis.m
        //cam.m_YAxis.Value = Mathf.Clamp(currentY,yMin, yMax );

        //currentX = cam.m_XAxis.Value;
        //cam.m_XAxis.Value = Mathf.Clamp(currentX,xMin, xMax );
        //Debug.Log(currentY);
        
    }
}
