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

    

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    //    Physics.IgnoreCollision(collision.collider2D, GetComponent<Collider>());
        if (collision.gameObject.tag == "Player" )
        {
            collision.gameObject.GetComponent<Player>().additem(this.GetComponent<Item>().ID);
            Destroy(this.gameObject);
        }
    }
}
