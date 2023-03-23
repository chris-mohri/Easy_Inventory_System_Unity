using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public InventorySystem inventory;
    public InventorySystem weapons;

    public Backpack backpack;

    [SerializeField] 
    public InventoryDisplayer inventoryDisplayer;

    void Awake()
    {
        inventory = new InventorySystem(24, "item"); // ---- \
        inventoryDisplayer.SetInventory(inventory);  // -----------  Main Inventory
        inventory.SetDisplayer(inventoryDisplayer);  // ---- /

        weapons = new InventorySystem(3, "weapon");


        backpack=new Backpack(inventory);
        backpack.AddInventory(weapons);


        //Debug.Log("wh");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log(inventory.ListItems());
       //Debug.Log("h");
    }
}
