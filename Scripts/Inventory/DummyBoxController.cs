using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DummyBoxController : MonoBehaviour
{
   
    [SerializeField]
    private TextMeshProUGUI stackNum;

    public void SetText(int stack)
    {
        
        stackNum.text=stack.ToString();

        
    }

}
