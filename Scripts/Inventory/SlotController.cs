using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SlotController : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
                              IPointerEnterHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
                            , IPointerExitHandler
{
    
    [SerializeField]
    private Image icon;

    [SerializeField]
    private GameObject iconObject;

    [SerializeField]
    private TextMeshProUGUI displayName;

    [SerializeField]
    private GameObject stackObj;

    [SerializeField]
    private TextMeshProUGUI stackLabel;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private GameObject HoldColor;

    //[SerializeField]
    private HandleDragAndDrop drag_drop;

    //// OTHER INFO
    private Vector2 initialDragXY;
    private InventoryItemData data;
    private int inventoryIndex;
    private InventorySystem inventory;
    private int stackNumber;
    //private int stackNumber=0; // when 0, will turn off image box;

    [SerializeField]
    public Sprite empty;

    //public event Action<SlotController> OnEndDragEvent;

    void Awake()
    {
        drag_drop=GameObject.FindWithTag("UI").GetComponent<HandleDragAndDrop>();
    }



    public void Set(InventoryItemWrapper item, int inventoryIndex,  InventorySystem inventory)
    {
        this.inventoryIndex=inventoryIndex; //this slot's respective index in inventory it belongs to
        this.inventory=inventory; //inventory this slot belongs to

        if (item!=null){ //if not an empty slot

            this.data=item.data; //this slot's item data
            icon.sprite = item.data.icon;
            //displayName.text = item.data.displayName;
            //description.text = item.data.description;
            
            stackNumber = item.stackSize;

            if (!drag_drop.dragStarted)
                stackObj.SetActive(true);
            if (stackNumber<=1)
            {
                stackObj.SetActive(false);
                return;
            }

            
            stackLabel.text = stackNumber.ToString();

        }

        //if its an empty slot
        else{
            this.data=null;
            icon.sprite = empty;

            stackObj.SetActive(false);
            stackLabel.text = "null";
        }

    }

    public void OnPointerDown( PointerEventData eventData )
     {
     }
 
     public void OnPointerUp( PointerEventData eventData )
     {

        
        if (drag_drop.dragStarted==true)    // if drag ended
        { 
         
            //drag_drop.SetDest(inventory, inventoryIndex);
            drag_drop.Swap();
        }
        drag_drop.SetFalse();

        if (inventory!=null)
            inventory.Refresh();
        
     }
 
     public void OnPointerClick( PointerEventData eventData )
     {
         

         if (inventory!=null)
            inventory.Refresh();
     }

     public void OnBeginDrag( PointerEventData eventData )
     {
        
        if (data != null)
        {
            //initialDragXY = icon.transform.position;
            //icon.transform.position=eventData.position;

            //turns off display of object
            stackObj.SetActive(false);
            iconObject.SetActive(false);
            

            drag_drop.SetSrc(inventory, inventoryIndex);
            drag_drop.SetTrue();

            //turns on picked up item
            drag_drop.SetDummyImage(icon.sprite);

            if (inventory!=null)
                inventory.Refresh();
        }
        
     }
     public void OnEndDrag( PointerEventData eventData )
     {
        //icon.transform.position = initialDragXY;

        //turns back on item display
        stackObj.SetActive(true);
        iconObject.SetActive(true);

        //hides dummyBox
        drag_drop.HideDummyBox();

        if (inventory!=null)
            inventory.Refresh();
        
     }
     public void OnDrag( PointerEventData eventData )
     {
        if (data != null)
        {
        //icon.transform.position=eventData.position;
        drag_drop.SetDummyStack(stackNumber);
        drag_drop.UpdateDummyBox(eventData.position);

        stackObj.SetActive(false);

        if (inventory!=null)
            inventory.Refresh();
        }
        
     }
     public void OnDrop( PointerEventData eventData )
     {
     }
     public void OnPointerEnter( PointerEventData eventData )
     {
        if (drag_drop.dragStarted==true){ //if drag started
            drag_drop.SetDest(inventory, inventoryIndex);
        }
        HoldColor.SetActive(true);

        if (inventory!=null)
            inventory.Refresh();
        
           
     }
     public void OnPointerExit( PointerEventData eventData )
     {
        if (drag_drop.dragStarted==true){ //if drag started
            drag_drop.SetDest(null, -1);
        }
        HoldColor.SetActive(false);

        if (inventory!=null)
            inventory.Refresh();
     }

 }

/**
    public void OnClick(BaseEventData data)
    {
        PointerEventData pData = (PointerEventData)data;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    **/

