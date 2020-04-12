using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public List<int> stackamount;
    public ItemDB itemDB;
    public GameObject canvas;
    public UIscript UIscript;
    public GameObject droplocation;

    private void Start()
    {
        itemDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        UIscript = GameObject.Find("Canvas").GetComponent<UIscript>();
        droplocation = gameObject.transform.GetChild(0).gameObject;
    }

    public void additem(int id)
    {
        StartCoroutine(UIscript.inventoryReset());
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
}
