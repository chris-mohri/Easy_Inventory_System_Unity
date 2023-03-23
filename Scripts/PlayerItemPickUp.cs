using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickUp : MonoBehaviour
{
    public GameObject pickUpRadius;
    public Player player;

    // Update is called once per frame


    void Start()
    {
        player = this.GetComponent<Player>();
        ////Debug.Log(player.inventory);
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collide");
        if (collision.tag=="Item")
        {
            ItemObject item = collision.gameObject.GetComponent<ItemObject>();
            player.backpack.Add(item.referenceItem, item.amount);
            //player.inventory.Refresh();
            item.OnHandlePickUpItem();
            
        }
    }


}
//