using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HandleDragAndDrop : MonoBehaviour
{
    // Start is called before the first frame update
    private int inventoryIndexSrc;
    private InventorySystem inventorySrc;
    private int inventoryIndexDest;
    private InventorySystem inventoryDest;

    public bool dragStarted=false;

    private Vector2 initialDummyBoxPosition;

    [SerializeField]
    public Transform dummyBox;


    [SerializeField]
    public GameObject dummyBoxStackObject;



    public void SetSrc(InventorySystem inventory, int inventoryIndex)
    {
        this.inventoryIndexSrc = inventoryIndex;
        this.inventorySrc = inventory;

    }

    public void SetDest(InventorySystem inventory, int inventoryIndex)
    {
        this.inventoryIndexDest = inventoryIndex;
        this.inventoryDest = inventory;

    }

    public void Swap()
    {
       //Debug.Log("Index Src: "+inventoryIndexSrc + "\nIndex Dest: " + inventoryIndexDest);
        // continues if item was dragged to different item slot
        if (inventoryIndexSrc!=-1 && inventoryIndexDest!=-1 && inventorySrc!=null && inventoryDest!=null &&(inventoryIndexDest!=inventoryIndexSrc || inventorySrc !=inventoryDest)) 
        {
 

            // if source item is null, return
            InventoryItemWrapper tempSrcItem = inventorySrc.GetItem(inventoryIndexSrc);
            if (tempSrcItem==null)
            {
                inventorySrc.Refresh();
                inventoryDest.Refresh();
                Clear();
            }

            // if src item is not null, swap items
            else
            {
                
                tempSrcItem = new InventoryItemWrapper(tempSrcItem.data, tempSrcItem.stackSize);
                //FIX HERE
                //if items are the same, add src item count to destination item count
                if (inventoryDest.GetItem(inventoryIndexDest)!=null && tempSrcItem.data.id==inventoryDest.GetItem(inventoryIndexDest).data.id)
                {
                    int num=tempSrcItem.stackSize;

                    //add to destination item stack, then set remainder into num
                    num = inventoryDest.AddToItemStack(inventoryIndexDest, num);
                    //Debug.Log(num);

                    if (num<=0) // if no remaining item, set to null
                    {
                        inventorySrc.SetItem(null, inventoryIndexSrc);
                    }
                    else    //if remaining item, set stack to remainder
                    {
                        
                        inventorySrc.SetItemStack(inventoryIndexSrc,num);
                    }

                    
                }

                else
                {
                    
                    inventorySrc.SetItem(inventoryDest.GetItem(inventoryIndexDest), inventoryIndexSrc);
                    inventoryDest.SetItem(tempSrcItem, inventoryIndexDest);
                }

                //update inventory display
                inventorySrc.Refresh();
                inventoryDest.Refresh();
                Clear();
            }

            
 
        }

       

        
    }

    //resets values
    public void Clear()
    {
        inventoryIndexSrc = -1;
        inventorySrc = null;
        inventoryIndexDest = -1;
        inventoryDest = null;
        dragStarted=false;
    }

    public void SetTrue(){
        dragStarted=true;
    }

    public void SetFalse(){
        dragStarted=false;
    }

    public void ShowDummy(bool show)
    {
        if (show){
            //dummyBox.SetActive(true);
        }

        else{
            //dummyBox.SetActive(false);
            dummyBox.transform.position=initialDummyBoxPosition;
        }
    }

    public void HideDummyBox()
    {
        dummyBox.transform.position=initialDummyBoxPosition;
        //SetDummyStack(0);
    }

    public void SetDummyImage(Sprite image)
    {
        //dummyBox.Find("Icon").GetComponent<Image>().sprite=image;
        dummyBox.GetComponent<Image>().sprite=image;
        //pogchamp
        
    }

    public void SetDummyStack(int stack)
    {
        if (stack>=2)
        {
            dummyBoxStackObject.SetActive(true);
            dummyBox.GetComponent<DummyBoxController>().SetText(stack);
        }
            
        else
        {
            dummyBoxStackObject.SetActive(false);
        }
    }

    public void UpdateDummyBox(Vector2 position){
        dummyBox.transform.position = position;
    }

    void Start()
    {
        initialDummyBoxPosition = dummyBox.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
