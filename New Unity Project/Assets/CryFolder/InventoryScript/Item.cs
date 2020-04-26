using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public int ID;
    public string ItemName;
    public Sprite icon;
    public int maxamount;
    public string type;
    public int damage;
    public int healing;
    public int durability;


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            Debug.Log("enter");
            collision.gameObject.GetComponent<PlayerInventory>().CanPickUp = true;
        }
    }

    public void pikcup(PlayerInventory inventory)
    {
        Debug.Log("pick");
        inventory.additem(this.GetComponent<Item>().ID);
        Destroy(this.gameObject);
        inventory.gameObject.GetComponent<PlayerInventory>().CanPickUp = false;
    }
}
