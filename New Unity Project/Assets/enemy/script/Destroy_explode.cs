using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_explode : MonoBehaviour
{   
    private Rigidbody2D rb;
    float dirX;
    float dirY;
    float torque;  

    // Start is called before the first frame update
  void Start()
    {
        dirX = Random.Range(-2f, 3f);
        dirY = Random.Range(2f, 3f);
        torque = 0.1f; /// Random.Range(1, 1);
        rb = GetComponent<Rigidbody2D> ();

        rb.AddForce (new Vector2 (dirX,dirY),ForceMode2D.Impulse);
        Physics2D.IgnoreLayerCollision(10,10);
        /// rb.AddTorque(torque, ForceMode2D.Impulse);

    }



   
    // Update is called once per frame
    void Update()
    {
        
    }
}
