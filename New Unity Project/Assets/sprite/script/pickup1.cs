﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup1 : MonoBehaviour
{
    public float omg;
    private Inventory1 inventory;
    public GameObject itemButton;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory1>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)

            {
                if (inventory.isFull[i] == false)
                {
                    Debug.Log(000);
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }


            }
        }
    }
}
