using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    //inventory
    public GameObject InventoryBoard;
    public Button[] slots;
    public bool inventory_opened = false;
    public PlayerInventory PlayerInventory;
    public Sprite buttonimg;

    //equipment
    public GameObject EquipmentBoard;
    public Text ProjectileNumber;
    public Image equipImg;

    public bool closeinventory()
    {
        EquipmentBoard.SetActive(false);
        InventoryBoard.SetActive(false);
        inventory_opened = false;
        ButtonClear();
        return false;
    }

    public void ButtonClear()
    {
        //clear all item set in InventorySetUp function (to reset inventory panel)
        for (int i = 0; i < slots.Length; i++)
        {
            Destroy(slots[i].gameObject.GetComponent<Item>());
        }
    }
    public bool openinventory()
    {
        inventorysetup(PlayerInventory.inventory);
        EquipmentBoard.SetActive(true);
        InventoryBoard.SetActive(true);
        inventory_opened = true;
        return true;
    }

    public void inventorysetup(List<Item> inventory)
    {
        for (int i = 0; i< inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                //set item slot image default image
                slots[i].image.sprite = buttonimg;
                break;
            }
            else
            {
                if (slots[i].gameObject.GetComponent<Item>() == null)
                {
                    //add item to each item slot
                    Item temp = slots[i].gameObject.AddComponent<Item>();
                    temp.ItemName = inventory[i].ItemName;
                    temp.ID = inventory[i].ID;
                    temp.icon = inventory[i].icon;
                    temp.type = inventory[i].type;
                    slots[i].image.sprite = temp.icon;
                    //set stack text
                    Text text = slots[i].gameObject.GetComponentInChildren<Text>();
                    text.text = PlayerInventory.stackamount[i].ToString();
                }
            }
        }
        //slot number assign and button set up
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].gameObject.GetComponent<inventoryButton>().slotnumber = i;
            slots[i].gameObject.GetComponent<inventoryButton>().ButtonSetUp();
        }
    }

    public void equipmentsetup(int stacknumber)
    {
        Debug.Log("equip set up");
        ProjectileNumber.text = stacknumber.ToString();

        if(stacknumber == 0 || stacknumber == 100)
        {
            ProjectileNumber.text = "";
            equipImg.enabled = false;
            equipImg.sprite = null;
        }
    }

    public IEnumerator inventoryReset()
    {
        ButtonClear();
        yield return new WaitForEndOfFrame();
        inventorysetup(PlayerInventory.inventory);
    }
}
