using System.Collections;
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
    public Button CloseButton;
    public Button UseButton;
    public GameObject EquipmentPanel;

    private void Start()
    {
        itemDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        EquipmentPanel = UIscript.gameObject.transform.Find("EquipmentPanel").gameObject;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UIscript = GameObject.Find("MainCanvas").GetComponent<UIscript>();
        text.text = player.stackamount[slotnumber].ToString();
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
        // to do: update item component
        Debug.Log("I used" + item.ItemName);
        if (item.type == "Projectile")
        {
            Debug.Log("proj");
            Image equipImg = EquipmentPanel.transform.Find("Equipped Image").GetComponent<Image>();
            equipImg.enabled = true;
            equipImg.sprite = item.icon;
        }

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
            player.inventory.RemoveAt(slotnumber) ;
            player.inventory.Add(null);
            player.stackamount.Remove(player.stackamount[slotnumber]);
            player.stackamount.Add(0);
        }

    }
}
