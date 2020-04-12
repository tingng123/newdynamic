﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryButton : MonoBehaviour
{
    public Item item;
    public Player player;
    public int slotnumber;
    public Text text;
    public UIscript UIscript;
    public ItemDB itemDB;

    private void Start()
    {
        itemDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
    }
    public void ButtonSetUp()
    {
        if (this.gameObject.GetComponent<Item>() != null)
        {
            item = this.gameObject.GetComponent<Item>();
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UIscript = GameObject.Find("Canvas").GetComponent<UIscript>();
        text.text = player.stackamount[slotnumber].ToString();
        //reset stack number in UI
        if (text.text == "0")
        {
            text.text = "";
        }
    }

    public void itemUse()
    {
        // to do: update item component
        Debug.Log("I used" + item.ItemName);
        player.stackamount[slotnumber] -= 1;
        StackChecking();
        StartCoroutine(UIscript.inventoryReset());
    }
    
    public void itemRemove()
    {
        Debug.Log("remove" +item.ItemName);
        player.stackamount[slotnumber] -= 1;
        StackChecking();
        StartCoroutine(UIscript.inventoryReset());
        //drop item
        Instantiate(itemDB.items[item.ID], player.droplocation.transform.position, Quaternion.identity);
    }

    public void StackChecking()
    {
        //check whether we used up all the stack
        if (player.stackamount[slotnumber] == 0)
        {
            Debug.Log(slotnumber);
            player.inventory.RemoveAt(slotnumber) ;
            Debug.Log("removed" + slotnumber);
            player.inventory.Add(null);
            player.stackamount.Remove(player.stackamount[slotnumber]);
            player.stackamount.Add(0);
        }

    }
}
