using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InventorySystem
{

    private Dictionary<InventoryItemData, InventoryItemWrapper> dictionary;

    [SerializeField]
    public List<InventoryItemWrapper> inventory {get; private set;}
    private int maxInventory;
    //private bool inventoryFull=false;
    
    ///////////  WEAPONS  ///////
    public List<InventoryItemWrapper> equippedWeapons {get; private set;}
    private int maxEquippedWeapons;
    ///////////  AMMO  //////////
    public List<InventoryItemWrapper> equippedAmmo {get; private set;}
    private int maxEquippedAmmo;

    private InventoryDisplayer inventoryDisplayer;

    public string type;

    //public InventoryItemWrapper blank;

    public InventorySystem(int size, string type)
    {

        //maxEquippedWeapons=2;
        //maxEquippedAmmo=2;
        this.maxInventory=size;
        this.type=type;

        inventory = new List<InventoryItemWrapper>();
        SetInventoryBlanks(inventory, maxInventory);


        //equippedWeapons = new List<InventoryItemWrapper>();
        //equippedAmmo = new List<InventoryItemWrapper>();
        

        
        
    }

    public void SetDisplayer(InventoryDisplayer displayer)
    {
        this.inventoryDisplayer=displayer;
    }

    public void Refresh()
    {
        if (inventoryDisplayer!=null)
            inventoryDisplayer.RefreshItems();
    }

    public void SetInventoryBlanks(List<InventoryItemWrapper> list, int num)
    {
        for (int i = 0; i<num; i++){
            list.Add(null);
        }

  
    }

    /**
    public InventoryItemWrapper Get(InventoryItemData referenceData)
    {
        if (dictionary.TryGetValue(referenceData, out InventoryItemWrapper value))
            return value;
        return null;
    } **/

    //gets size of list (does not count nulls)
    public int GetSize(List<InventoryItemWrapper> list){

        int count=0;
        foreach(InventoryItemWrapper item in list){
            
            if (item!=null)
            {
                count++;
            }
        }

        return count;
    }

    //tries to add item to inventory then returns the remaining amount
    public int Add(InventoryItemData referenceData, int num)
    {

        /**
        //if item is weapon and player has empty weapon slot, automatically go into empty weapon slot
        if (referenceData.type == "weapon"){
            if (equippedWeapons.Count<maxEquippedWeapons)
            {
                equippedWeapons.Add(new InventoryItemWrapper(referenceData, num));
                return;
            }
        }

        if (num>0 && referenceData.type=="ammo"){
            
            InventoryItemWrapper ammo;
            //loops through each space of ammo inventory
            for (int i =0; i<maxEquippedAmmo; i++)
            {

                ammo=equippedAmmo[i];

                if (ammo==null) //if empty space
                {
                    equippedAmmo[i]=new InventoryItemWrapper(referenceData, num);
                    num=0;
                }

                //if not empty space but is the same type of ammo
                else if (ammo.data.id==referenceData.id)
                {
                    num=ammo.AddToStack(num);
                }
                
                // if no more item to distribute to equipped slots or inventory
                if (num<=0)
                    return;

            }
        }
        **/
        // if there's more items to distribute in inventory
        if(num>0 && (this.type=="item" || this.type==referenceData.type))
        {
            InventoryItemWrapper item;
            //loops through each space of ammo inventory
            for (int i =0; i<maxInventory; i++)
            {
                item=inventory[i];

                if (item==null) //if empty space
                {
                    inventory[i]=new InventoryItemWrapper(referenceData, num);
                    num=0;
                }

                // if item is the same type as the one picked up
                else if (item.data.id==referenceData.id)
                {
                    num=item.AddToStack(num);
                }
                
                // if no more item to distribute to  inventory
                if (num<=0)
                    break;
            }

            //////////////////////////////////////////////////////////////////////////


            //if (type=="item")
                //drop item

            // ADD FUNCTIONALITY TO DROP EXCESS AMOUNT
            // A;LSDKFJA;SLDKFJA;SLKFJSDAD;ALSKDFJ

            //                      ADD HERE

            //////////////////////////////////////////////////////////////////////////



            ////////////////////////////////////////////////////////////////////////////

            //                      refreshInventory
            //update inventory display after changing inventory
            
            

            ////////////////////////////////////////////////////////////////////////////
        
        }

        Refresh();

        return num;
      
        
    
    }

    public void Remove(InventoryItemData referenceData, int num)
    {
        if (dictionary.TryGetValue(referenceData, out InventoryItemWrapper value))
        {

            value.RemoveFromStack(num);

            if (value.stackSize <= 0)
            {
                inventory.Remove(value);
                dictionary.Remove(referenceData);
            }

        }
    }

    public string ListItems()
    {
        
    /**
    var items = from kvp in dictionary
                select kvp.Key + "=" + kvp.Value;

    return "{" + string.Join(",", items) + "}";

    **/

    string line="";

    foreach(InventoryItemWrapper item in inventory)
    {
        line.Concat(item.data.displayName + "\n");
        //Debug.Log(line);
    }
    return line;
    

    }

    public List<InventoryItemWrapper> getItemList()
    {
        return inventory;
    }

    public InventoryItemWrapper GetItem(int index)
    {
        return inventory[index];
    }

    public void SetItemStack(int index, int num)
    {
        if (inventory[index]!=null)
        {
            
            inventory[index].SetStack(num);
        
        }
  
    }

    public int AddToItemStack(int index, int num)
    {
        if (inventory[index]!=null)
        {
            num = inventory[index].AddToStack(num);
            return num;
        }
        return -1;
    }

    public void SetItem(InventoryItemWrapper item, int index)
    {
        inventory[index] = item;
    }

    public int getCount(List<InventoryItemWrapper> list)
    {
        int count = 0;
        foreach (InventoryItemWrapper item in list)
        {
            if (item!=null)
                count++;
        }

        return count;
    }
}
