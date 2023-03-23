using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InventoryItemWrapper
{
    public InventoryItemData data {get; private set;}
    public int stackSize {get; private set;} // number in the stack

    public InventoryItemWrapper(InventoryItemData source, int num)
    {
        data = source;

        if (source!=null)
            AddToStack(num);

    }
    //                                                            to add +current/maxstack
    //adds to stack and returns(if applicable) the overflow amount --> ex. 10+62 / 64 returns 8
    public int AddToStack(int num)
    {
        int tempSum = stackSize+num;
        
        //return overflow val
        if (tempSum>data.maxStack)
        {
            stackSize=data.maxStack;
            //Debug.Log(tempSum-data.maxStack);
            return tempSum-data.maxStack;
        }

        //returns non overflow val
        else {
            stackSize=tempSum;
            return 0;
        }

  
    }

    public void SetStack(int num)
    {
        stackSize=num;

        if (stackSize>data.maxStack)
            stackSize=data.maxStack;
    }

    public void RemoveFromStack(int num)
    {
        stackSize-=num;
        if (stackSize<0)
            stackSize=0;
    }
}
