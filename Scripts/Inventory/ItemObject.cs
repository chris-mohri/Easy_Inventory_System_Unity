using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    public int amount = 1;

    void Start()
    {
        //player = 
    }

    public void setAmount(int num)
    {
        amount=num;
    }

    public void setRandAmount(int low, int high)
    {
        amount = Random.Range(low, high+1);
    }

    public void OnHandlePickUpItem()
    {
        //InventorySystem.Add(referenceItem, amount);
        Destroy(gameObject);
    }
/**
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide");
    }
    **/

/**
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collission.tag=="Player")
        {
        }
    }
    **/


}
