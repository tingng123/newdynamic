using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public List<int> stackamount;
    public ItemDB itemDB;
    public GameObject canvas;
    public InventoryScript InventoryScript;
    public GameObject droplocation;
    public bool CanPickUp;
    public GameObject pickupobj = null;

    public Item EquippedItem;

    //100 = no equipment
    public int EquippedSlotNumber;

    private void Start()
    {
        itemDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        InventoryScript = GameObject.Find("MainCanvas").GetComponent<InventoryScript>();
        InventoryScript.PlayerInventory = this;
        droplocation = gameObject.transform.GetChild(0).gameObject;
        //for (int i = 0; i < InventoryScript.slots.Length; i++)
        //{
        //    InventoryScript.slots[i].gameObject.GetComponent<inventoryButton>().InventoryScript = 
        //}

    }

    public void additem(int id)
    {
        StartCoroutine(InventoryScript.inventoryReset());
        for (int i = 0; i < inventory.Count; i++)
        {
            //there is item inside inventory of player
            if (inventory[i] != null)
            {
                // there is "same" item inside inventory
                if (inventory[i].ID == id)
                {
                    //check whether we can stack it up
                    if (stackamount[i] == inventory[i].maxamount)
                    {
                        Debug.Log("full stack");
                    }
                    else
                    {
                        //stack
                        stackamount[i] += 1;
                        break;
                    }
                }
            }
            //cant stack-> normal new item adding process
            if (inventory[i] == null)
            {
                inventory[i] = itemDB.items[id];
                stackamount[i] += 1;
                break;
            }
        }
    }

    public bool StackChecking(int slotnumber)
    {
        //check whether we used up all the stack
        if (stackamount[slotnumber] == 0)
        {
           inventory.RemoveAt(slotnumber);
           inventory.Add(null);
           stackamount.Remove(stackamount[slotnumber]);
           stackamount.Add(0);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            CanPickUp = true;
            pickupobj = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            CanPickUp = false;
            pickupobj = null;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanPickUp == true)
        {
            Debug.Log("pick up");
            pickupobj.gameObject.GetComponent<Item>().pikcup(this);
        }
    }
}
