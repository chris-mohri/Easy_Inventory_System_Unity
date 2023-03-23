using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    // Start is called before the first frame update
    //public Player player;

    private InventorySystem inventory;
    //private RectTransform itemContainer; 
    public GameObject inventoryPanel;



    public void SetInventory(InventorySystem inventory)
    {
        this.inventory=inventory;
    }

    public void RefreshItems()
    {
        int i = 0;
        GameObject child;
        //string childName="ItemHolder";


        foreach(InventoryItemWrapper item in inventory.getItemList())
        {
            
            child=inventoryPanel.transform.GetChild(i).gameObject;
            child.GetComponent<SlotController>().Set(item, i, inventory);

            i++;

        }
    }

    void Update()
    {
        //RefreshItems();
    }

    public void Onclick(BaseEventData data)
    {

        //int childCount = inventoryPanel.transform.childCount;

        //Debug.Log("hello");
    }


    
}
