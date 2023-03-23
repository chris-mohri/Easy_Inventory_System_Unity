using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack
{
    // Start is called before the first frame update
    public List<InventorySystem> otherInventories {get; private set;}
    public InventorySystem mainInventory {get;private set;}


    public Backpack(InventorySystem inventory)
    {
        this.mainInventory=inventory;
        otherInventories = new List<InventorySystem>();
    }

    public void AddInventory(InventorySystem inventory)
    {
        otherInventories.Add(inventory);
    }

    //adds item to respective type's designated inventory, then adds the remainder to the general inventory
    public void Add(InventoryItemData item, int num)
    {
        //goes through other inventories e.g. weaponInventory, ammoinventory, etc.
        foreach (InventorySystem inventory in otherInventories) 
        {
            if (inventory.type==item.type) //if item type matches the inventory
            {
                num = inventory.Add(item, num);
                break;
            }
        
        }

        //if there's still items remaining, add to main inventory; if main inventory is full, .Add() will drop the remainder
        if (num>0)
            mainInventory.Add(item, num);

    }
}
