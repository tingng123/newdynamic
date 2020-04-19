using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryButton : MonoBehaviour
{
    public Item item;
    public PlayerInventory PlayerInventory;
    public int slotnumber;
    public Text text;
    public InventoryScript InventoryScript;
    public ItemDB itemDB;
    public Button CloseButton;
    public Button UseButton;
    public GameObject EquipmentPanel;

    private void Start()
    {
        itemDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        EquipmentPanel = InventoryScript.gameObject.transform.Find("InGameObj").gameObject.transform.Find("EquipmentPanel").gameObject;
    }

    public void ButtonSetUp()
    {
        CloseButton = gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
        UseButton = gameObject.GetComponent<Button>();

        if (this.gameObject.GetComponent<Item>() != null)
        {
            item = this.gameObject.GetComponent<Item>();
            CloseButton.gameObject.SetActive(true);
            UseButton.enabled = true;
        }
        PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        InventoryScript = GameObject.Find("MainCanvas").GetComponent<InventoryScript>();
        text.text = PlayerInventory.stackamount[slotnumber].ToString();
        //reset stack number in UI
        if (text.text == "0")
        {
            text.text = "";
            CloseButton.gameObject.SetActive(false);
            UseButton.enabled = false;
        }
    }

    public void itemUse()
    {
        Debug.Log("I used" + item.ItemName);
        if (item.type == "Projectile")
        {
            Debug.Log("proj");
            PlayerInventory.EquippedSlotNumber = slotnumber;

            PlayerInventory.EquippedItem = PlayerInventory.inventory[slotnumber];
            InventoryScript.equipImg = EquipmentPanel.transform.Find("Equipped Image").GetComponent<Image>();
            InventoryScript.equipImg.enabled = true;
            InventoryScript.equipImg.sprite = PlayerInventory.EquippedItem.icon;
            //player.stackamount[slotnumber] -= 1;
            PlayerInventory.StackChecking(slotnumber);
            StartCoroutine(InventoryScript.inventoryReset());

            // become zero
            if (PlayerInventory.StackChecking(slotnumber) == false){
                InventoryScript.equipImg.enabled = false;
                InventoryScript.equipImg.sprite = null;
            }
            InventoryScript.equipmentsetup(PlayerInventory.stackamount[PlayerInventory.EquippedSlotNumber]);
        }
        else
        {
            PlayerInventory.stackamount[slotnumber] -= 1;
            PlayerInventory.StackChecking(slotnumber);
            StartCoroutine(InventoryScript.inventoryReset());
        }
    }
    
    public void itemRemove()
    {
        Debug.Log("remove" +item.ItemName);
        PlayerInventory.stackamount[slotnumber] -= 1;
        PlayerInventory.StackChecking(slotnumber);
        StartCoroutine(InventoryScript.inventoryReset());
        //drop item
        Instantiate(itemDB.items[item.ID], PlayerInventory.droplocation.transform.position, Quaternion.identity);
    }

}
